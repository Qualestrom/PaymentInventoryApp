# ==============================================================================
# Automated Payment and Inventory Management System
# Application Logic Tier: Inventory Engine (inventory_engine.py)
# Handles FIFO Stock Deduction, Expiration Warnings, Smart Alerts, and Tax/Discount Math
# ==============================================================================

import sys
import json
import datetime
from typing import Dict, Any, List

from config import (
    EXPIRY_WARNING_DAYS,
    DEFAULT_TAX_RATE
)
import db_bridge


def lookup_product_with_smart_alert(params: Dict[str, Any]) -> Dict[str, Any]:
    """
    Looks up product details and checks active batches for imminent expiration.
    Sets 'is_near_expiry' and 'days_until_expiry' to trigger Cashier Smart Alerts.
    """
    prod_res = db_bridge.lookup_product(params)
    if not prod_res.get("success"):
        return prod_res

    prod = prod_res["data"]
    product_id = prod["product_id"]

    # Check active batches in PostgreSQL or mock store
    is_near_expiry = False
    min_days_remaining = None
    near_expiry_batch_id = None

    conn = db_bridge.get_postgres_connection()
    if conn:
        try:
            cursor = conn.cursor()
            cursor.execute(
                """SELECT batch_id, expiry_date, (expiry_date - CURRENT_DATE) as days_remaining 
                   FROM inventory 
                   WHERE product_id = %s AND quantity > 0 
                   ORDER BY delivery_date ASC, expiry_date ASC""",
                (product_id,)
            )
            batches = cursor.fetchall()
            cursor.close()
            conn.close()

            for b in batches:
                b_id, exp_date, days_rem = b[0], b[1], int(b[2])
                if days_rem <= EXPIRY_WARNING_DAYS:
                    is_near_expiry = True
                    if min_days_remaining is None or days_rem < min_days_remaining:
                        min_days_remaining = days_rem
                        near_expiry_batch_id = b_id
        except Exception:
            pass
    else:
        # Fallback to mock batches
        today = datetime.date.today()
        for b in db_bridge.MOCK_INVENTORY_BATCHES:
            if b["product_id"] == product_id and b["quantity"] > 0:
                exp_date = datetime.date.fromisoformat(b["expiry_date"])
                days_rem = (exp_date - today).days
                if days_rem <= EXPIRY_WARNING_DAYS:
                    is_near_expiry = True
                    if min_days_remaining is None or days_rem < min_days_remaining:
                        min_days_remaining = days_rem
                        near_expiry_batch_id = b["batch_id"]

    prod["is_near_expiry"] = is_near_expiry
    prod["days_until_expiry"] = min_days_remaining if is_near_expiry else None
    prod["near_expiry_batch_id"] = near_expiry_batch_id

    return {"success": True, "data": prod}


def calculate_transaction(params: Dict[str, Any]) -> Dict[str, Any]:
    """
    Computes Subtotal, Discounts (Senior/PWD/Promo), Taxable Amount, 12% VAT, and Net Total.
    """
    items = params.get("items", [])
    tax_rate = float(params.get("tax_rate", DEFAULT_TAX_RATE))
    discount_input = float(params.get("discount_percent", 0.0))

    # Normalize discount percentage (e.g. 20 -> 0.20, or 0.20 -> 0.20)
    discount_fraction = discount_input / 100.0 if discount_input > 1.0 else discount_input
    discount_fraction = max(0.0, min(1.0, discount_fraction))

    subtotal = 0.0
    for it in items:
        qty = int(it.get("quantity", 1))
        price = float(it.get("unit_price", 0.0))
        subtotal += qty * price

    subtotal = round(subtotal, 2)
    discount_amount = round(subtotal * discount_fraction, 2)
    taxable_amount = round(max(0.0, subtotal - discount_amount), 2)
    tax_amount = round(taxable_amount * tax_rate, 2)
    total = round(taxable_amount + tax_amount, 2)

    return {
        "success": True,
        "data": {
            "subtotal": subtotal,
            "discount_percent": round(discount_fraction * 100.0, 2),
            "discount_amount": discount_amount,
            "taxable_amount": taxable_amount,
            "tax_rate": tax_rate,
            "tax_amount": tax_amount,
            "total": total
        }
    }


def deduct_stock_fifo(params: Dict[str, Any]) -> Dict[str, Any]:
    """
    Deducts inventory using First-In, First-Out (FIFO) discipline.
    Consumes oldest delivery date batches first.
    Transactional: If any item has insufficient stock, the entire deduction fails.
    """
    items = params.get("items", [])
    if not items:
        return {"success": False, "error": "No items provided for stock deduction."}

    # Aggregate quantities requested per product_id
    requested_by_pid: Dict[int, int] = {}
    for it in items:
        pid = int(it["product_id"])
        qty = int(it["quantity"])
        requested_by_pid[pid] = requested_by_pid.get(pid, 0) + qty

    conn = db_bridge.get_postgres_connection()
    if conn:
        try:
            conn.autocommit = False
            cursor = conn.cursor()

            # 1. Verification Phase: Check total stock availability
            for pid, req_qty in requested_by_pid.items():
                cursor.execute(
                    "SELECT COALESCE(SUM(quantity), 0) FROM inventory WHERE product_id = %s",
                    (pid,)
                )
                avail = cursor.fetchone()[0]
                if avail < req_qty:
                    conn.rollback()
                    cursor.close()
                    conn.close()
                    return {
                        "success": False,
                        "error": f"Insufficient stock for Product ID {pid}. Available: {avail}, Requested: {req_qty}"
                    }

            # 2. FIFO Deduction Phase
            deduction_log = []
            for pid, req_qty in requested_by_pid.items():
                cursor.execute(
                    """SELECT batch_id, quantity, delivery_date 
                       FROM inventory 
                       WHERE product_id = %s AND quantity > 0 
                       ORDER BY delivery_date ASC, batch_id ASC FOR UPDATE""",
                    (pid,)
                )
                batches = cursor.fetchall()

                qty_needed = req_qty
                batches_deducted = 0
                for b in batches:
                    b_id, b_qty = b[0], b[1]
                    if qty_needed <= 0:
                        break

                    deduct_here = min(b_qty, qty_needed)
                    new_qty = b_qty - deduct_here
                    cursor.execute("UPDATE inventory SET quantity = %s WHERE batch_id = %s", (new_qty, b_id))

                    qty_needed -= deduct_here
                    batches_deducted += 1

                deduction_log.append({
                    "product_id": pid,
                    "quantity_deducted": req_qty,
                    "batches_affected": batches_deducted
                })

            conn.commit()
            cursor.close()
            conn.close()
            return {"success": True, "data": {"deductions": deduction_log}}
        except Exception as ex:
            if conn:
                conn.rollback()
                conn.close()
            return {"success": False, "error": f"PostgreSQL FIFO Deduction Error: {str(ex)}"}

    # Mock Fallback FIFO implementation
    # 1. Verify
    for pid, req_qty in requested_by_pid.items():
        total_avail = sum(b["quantity"] for b in db_bridge.MOCK_INVENTORY_BATCHES if b["product_id"] == pid)
        if total_avail < req_qty:
            return {
                "success": False,
                "error": f"Insufficient stock for Product ID {pid}. Available: {total_avail}, Requested: {req_qty}"
            }

    # 2. Deduct in FIFO order (sorted by delivery_date ASC, batch_id ASC)
    deduction_log = []
    # Sort mock batches
    db_bridge.MOCK_INVENTORY_BATCHES.sort(key=lambda x: (x["delivery_date"], x["batch_id"]))

    for pid, req_qty in requested_by_pid.items():
        qty_needed = req_qty
        batches_deducted = 0

        for b in db_bridge.MOCK_INVENTORY_BATCHES:
            if b["product_id"] == pid and b["quantity"] > 0:
                if qty_needed <= 0:
                    break
                deduct_here = min(b["quantity"], qty_needed)
                b["quantity"] -= deduct_here
                qty_needed -= deduct_here
                batches_deducted += 1

        deduction_log.append({
            "product_id": pid,
            "quantity_deducted": req_qty,
            "batches_affected": batches_deducted
        })

    return {"success": True, "data": {"deductions": deduction_log}}


# ==============================================================================
# CLI and Dispatch Router (JSON IPC)
# ==============================================================================
ACTION_DISPATCH = {
    "lookup_product": lookup_product_with_smart_alert,
    "calculate_transaction": calculate_transaction,
    "deduct_stock": deduct_stock_fifo,
    "check_expiry": db_bridge.get_expiring_soon
}


def dispatch(payload: Dict[str, Any]) -> Dict[str, Any]:
    action = payload.get("action")
    params = payload.get("params", {})

    handler = ACTION_DISPATCH.get(action)
    if not handler:
        # Fallback check on db_bridge actions
        if action in db_bridge.ACTION_DISPATCH:
            return db_bridge.ACTION_DISPATCH[action](params)
        return {"success": False, "error": f"Unknown inventory action: '{action}'"}

    try:
        return handler(params)
    except Exception as ex:
        return {"success": False, "error": f"Exception in inventory_engine '{action}': {str(ex)}"}


def main():
    """
    CLI / Process entry point.
    Accepts JSON from CLI argument or stdin, prints JSON output.
    """
    input_str = ""
    if len(sys.argv) > 1:
        input_str = sys.argv[1]
    else:
        input_str = sys.stdin.read().strip()

    if not input_str:
        print(json.dumps({"success": False, "error": "No input JSON received."}))
        return

    try:
        payload = json.loads(input_str)
    except Exception as ex:
        print(json.dumps({"success": False, "error": f"JSON parse error: {str(ex)}"}))
        return

    response = dispatch(payload)
    print(json.dumps(response))


if __name__ == "__main__":
    main()
