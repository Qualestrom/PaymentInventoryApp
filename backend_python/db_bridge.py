# ==============================================================================
# Automated Payment and Inventory Management System
# Application Logic Tier: Database Bridge (db_bridge.py)
# Connects to MySQL (store_sales) and PostgreSQL (store_inventory)
# Handles Authentication, Product/Price Updates, Sales Recording, Stock Logging
# ==============================================================================

import sys
import json
import hashlib
import datetime
from typing import Dict, Any, List, Optional

try:
    import mysql.connector
    HAS_MYSQL_DRIVER = True
except ImportError:
    HAS_MYSQL_DRIVER = False

try:
    import psycopg2
    from psycopg2.extras import RealDictCursor
    HAS_PG_DRIVER = True
except ImportError:
    HAS_PG_DRIVER = False

from config import (
    MYSQL_CONFIG,
    POSTGRES_CONFIG,
    LOW_STOCK_THRESHOLD,
    EXPIRY_WARNING_DAYS
)


# ==============================================================================
# In-Memory Mock Store (Graceful Fallback if Databases are Offline/Not Installed)
# ==============================================================================
MOCK_USERS = [
    {
        "user_id": 1,
        "username": "admin",
        # SHA-256 for 'admin123'
        "password_hash": hashlib.sha256("admin123".encode("utf-8")).hexdigest(),
        "role": "Manager",
        "full_name": "Administrator"
    },
    {
        "user_id": 2,
        "username": "cashier1",
        # SHA-256 for 'cashier123'
        "password_hash": hashlib.sha256("cashier123".encode("utf-8")).hexdigest(),
        "role": "Cashier",
        "full_name": "Juan Dela Cruz"
    }
]

MOCK_PRODUCTS = [
    {"product_id": 1, "barcode": "4901234567890", "name": "Whole Milk 1L", "price": 89.75},
    {"product_id": 2, "barcode": "4902345678901", "name": "White Bread Loaf", "price": 65.00},
    {"product_id": 3, "barcode": "4903456789012", "name": "Canned Tuna 155g", "price": 38.50},
    {"product_id": 4, "barcode": "4904567890123", "name": "Instant Coffee 200g", "price": 245.00},
    {"product_id": 5, "barcode": "4905678901234", "name": "Bottled Water 500mL", "price": 15.00}
]

today = datetime.date.today()
MOCK_INVENTORY_BATCHES = [
    # Milk
    {"batch_id": 1, "product_id": 1, "quantity": 15, "delivery_date": (today - datetime.timedelta(days=10)).isoformat(), "expiry_date": (today + datetime.timedelta(days=2)).isoformat()},
    {"batch_id": 2, "product_id": 1, "quantity": 25, "delivery_date": (today - datetime.timedelta(days=5)).isoformat(),  "expiry_date": (today + datetime.timedelta(days=12)).isoformat()},
    {"batch_id": 3, "product_id": 1, "quantity": 40, "delivery_date": (today - datetime.timedelta(days=1)).isoformat(),  "expiry_date": (today + datetime.timedelta(days=20)).isoformat()},
    # Bread
    {"batch_id": 4, "product_id": 2, "quantity": 10, "delivery_date": (today - datetime.timedelta(days=4)).isoformat(),  "expiry_date": (today + datetime.timedelta(days=3)).isoformat()},
    {"batch_id": 5, "product_id": 2, "quantity": 20, "delivery_date": (today - datetime.timedelta(days=2)).isoformat(),  "expiry_date": (today + datetime.timedelta(days=6)).isoformat()},
    {"batch_id": 6, "product_id": 2, "quantity": 30, "delivery_date": today.isoformat(),                                   "expiry_date": (today + datetime.timedelta(days=10)).isoformat()},
    # Tuna
    {"batch_id": 7, "product_id": 3, "quantity": 50, "delivery_date": (today - datetime.timedelta(days=60)).isoformat(), "expiry_date": (today + datetime.timedelta(days=500)).isoformat()},
    {"batch_id": 8, "product_id": 3, "quantity": 40, "delivery_date": (today - datetime.timedelta(days=30)).isoformat(), "expiry_date": (today + datetime.timedelta(days=530)).isoformat()},
    {"batch_id": 9, "product_id": 3, "quantity": 60, "delivery_date": (today - datetime.timedelta(days=5)).isoformat(),  "expiry_date": (today + datetime.timedelta(days=600)).isoformat()},
    # Coffee (low stock)
    {"batch_id": 10, "product_id": 4, "quantity": 3, "delivery_date": (today - datetime.timedelta(days=90)).isoformat(), "expiry_date": (today + datetime.timedelta(days=300)).isoformat()},
    {"batch_id": 11, "product_id": 4, "quantity": 4, "delivery_date": (today - datetime.timedelta(days=45)).isoformat(), "expiry_date": (today + datetime.timedelta(days=360)).isoformat()},
    # Water
    {"batch_id": 12, "product_id": 5, "quantity": 50, "delivery_date": (today - datetime.timedelta(days=20)).isoformat(), "expiry_date": (today + datetime.timedelta(days=365)).isoformat()},
    {"batch_id": 13, "product_id": 5, "quantity": 50, "delivery_date": (today - datetime.timedelta(days=10)).isoformat(), "expiry_date": (today + datetime.timedelta(days=375)).isoformat()},
    {"batch_id": 14, "product_id": 5, "quantity": 100, "delivery_date": (today - datetime.timedelta(days=1)).isoformat(), "expiry_date": (today + datetime.timedelta(days=390)).isoformat()}
]

MOCK_SALES_TRANSACTIONS: List[Dict[str, Any]] = []


# ==============================================================================
# Database Connection Helpers with Quick Socket Probe
# ==============================================================================
import socket

def _is_port_open(host: str, port: int, timeout: float = 0.4) -> bool:
    try:
        with socket.create_connection((host, port), timeout=timeout):
            return True
    except (socket.timeout, ConnectionRefusedError, OSError):
        return False

def get_mysql_connection():
    if not HAS_MYSQL_DRIVER:
        return None
    if not _is_port_open(MYSQL_CONFIG["host"], MYSQL_CONFIG["port"]):
        return None
    try:
        conn = mysql.connector.connect(
            host=MYSQL_CONFIG["host"],
            port=MYSQL_CONFIG["port"],
            user=MYSQL_CONFIG["user"],
            password=MYSQL_CONFIG["password"],
            database=MYSQL_CONFIG["database"],
            connection_timeout=MYSQL_CONFIG["connect_timeout"]
        )
        return conn
    except Exception:
        return None

def get_postgres_connection():
    if not HAS_PG_DRIVER:
        return None
    if not _is_port_open(POSTGRES_CONFIG["host"], POSTGRES_CONFIG["port"]):
        return None
    try:
        conn = psycopg2.connect(
            host=POSTGRES_CONFIG["host"],
            port=POSTGRES_CONFIG["port"],
            user=POSTGRES_CONFIG["user"],
            password=POSTGRES_CONFIG["password"],
            dbname=POSTGRES_CONFIG["database"],
            connect_timeout=POSTGRES_CONFIG["connect_timeout"]
        )
        return conn
    except Exception:
        return None


# ==============================================================================
# Action Handlers
# ==============================================================================

def authenticate_user(params: Dict[str, Any]) -> Dict[str, Any]:
    """
    Authenticates user credentials against the MySQL users table.
    Enforces Role-Based Access Control (RBAC).
    """
    username = params.get("username", "").strip()
    password = params.get("password", "").strip()

    if not username or not password:
        return {"success": False, "error": "Username and password are required."}

    password_hash = hashlib.sha256(password.encode("utf-8")).hexdigest()

    conn = get_mysql_connection()
    if conn:
        try:
            cursor = conn.cursor(dictionary=True)
            cursor.execute(
                "SELECT user_id, username, password_hash, role, full_name FROM users WHERE username = %s",
                (username,)
            )
            user = cursor.fetchone()
            cursor.close()
            conn.close()

            if user and user["password_hash"] == password_hash:
                return {
                    "success": True,
                    "data": {
                        "user_id": user["user_id"],
                        "username": user["username"],
                        "role": user["role"],
                        "full_name": user["full_name"]
                    }
                }
            return {"success": False, "error": "Invalid username or password."}
        except Exception as ex:
            return {"success": False, "error": f"MySQL Auth Error: {str(ex)}"}

    # Mock Fallback
    for u in MOCK_USERS:
        if u["username"].lower() == username.lower() and u["password_hash"] == password_hash:
            return {
                "success": True,
                "data": {
                    "user_id": u["user_id"],
                    "username": u["username"],
                    "role": u["role"],
                    "full_name": u["full_name"]
                }
            }
    return {"success": False, "error": "Invalid username or password."}


def get_all_products(params: Optional[Dict[str, Any]] = None) -> Dict[str, Any]:
    """Retrieves all products from the MySQL products table."""
    conn = get_mysql_connection()
    if conn:
        try:
            cursor = conn.cursor(dictionary=True)
            cursor.execute("SELECT product_id, barcode, name, price FROM products ORDER BY product_id ASC")
            rows = cursor.fetchall()
            cursor.close()
            conn.close()
            # Convert decimal to float
            for r in rows:
                r["price"] = float(r["price"])
            return {"success": True, "data": rows}
        except Exception as ex:
            return {"success": False, "error": f"MySQL Product Query Error: {str(ex)}"}

    # Mock Fallback
    return {"success": True, "data": [dict(p) for p in MOCK_PRODUCTS]}


def lookup_product(params: Dict[str, Any]) -> Dict[str, Any]:
    """Looks up a single product by barcode or product_id."""
    barcode = params.get("barcode")
    product_id = params.get("product_id")

    conn = get_mysql_connection()
    if conn:
        try:
            cursor = conn.cursor(dictionary=True)
            if barcode:
                cursor.execute("SELECT product_id, barcode, name, price FROM products WHERE barcode = %s", (barcode,))
            elif product_id:
                cursor.execute("SELECT product_id, barcode, name, price FROM products WHERE product_id = %s", (product_id,))
            else:
                return {"success": False, "error": "Neither barcode nor product_id was provided."}

            row = cursor.fetchone()
            cursor.close()
            conn.close()
            if row:
                row["price"] = float(row["price"])
                return {"success": True, "data": row}
            return {"success": False, "error": f"Product not found for: {barcode or product_id}"}
        except Exception as ex:
            return {"success": False, "error": f"MySQL Query Error: {str(ex)}"}

    # Mock Fallback
    for p in MOCK_PRODUCTS:
        if (barcode and p["barcode"] == str(barcode)) or (product_id and p["product_id"] == int(product_id)):
            return {"success": True, "data": dict(p)}
    return {"success": False, "error": f"Product not found for: {barcode or product_id}"}


def update_product_price(params: Dict[str, Any]) -> Dict[str, Any]:
    """
    Updates the unit price of a product in MySQL.
    Strictly enforced: Only users with role 'Manager' are authorized.
    """
    role = params.get("role")
    if role != "Manager":
        return {"success": False, "error": "Authorization denied. Only Manager role may edit prices."}

    product_id = params.get("product_id")
    new_price = params.get("new_price")

    if product_id is None or new_price is None:
        return {"success": False, "error": "Missing product_id or new_price parameter."}

    try:
        new_price = float(new_price)
        if new_price <= 0:
            return {"success": False, "error": "Price must be a positive number."}
    except ValueError:
        return {"success": False, "error": "Invalid price format."}

    conn = get_mysql_connection()
    if conn:
        try:
            cursor = conn.cursor()
            cursor.execute("UPDATE products SET price = %s WHERE product_id = %s", (new_price, product_id))
            conn.commit()
            affected = cursor.rowcount
            cursor.close()
            conn.close()
            if affected > 0:
                return {"success": True, "data": {"message": f"Price updated to {new_price:.2f}"}}
            return {"success": False, "error": f"Product ID {product_id} not found."}
        except Exception as ex:
            return {"success": False, "error": f"MySQL Update Error: {str(ex)}"}

    # Mock Fallback
    for p in MOCK_PRODUCTS:
        if p["product_id"] == int(product_id):
            p["price"] = new_price
            return {"success": True, "data": {"message": f"Price updated to {new_price:.2f}"}}
    return {"success": False, "error": f"Product ID {product_id} not found in mock store."}


def record_sale(params: Dict[str, Any]) -> Dict[str, Any]:
    """
    Records a completed checkout transaction into MySQL (sales_transactions + line items).
    """
    cashier_id = params.get("cashier_id")
    subtotal = float(params.get("subtotal", 0.0))
    tax_amount = float(params.get("tax_amount", 0.0))
    discount_percent = float(params.get("discount_percent", 0.0))
    total_amount = float(params.get("total_amount", 0.0))
    items = params.get("items", [])

    if not items:
        return {"success": False, "error": "Cannot record sale with empty items list."}

    conn = get_mysql_connection()
    if conn:
        try:
            conn.start_transaction()
            cursor = conn.cursor()
            cursor.execute(
                """INSERT INTO sales_transactions 
                   (date, subtotal, tax_amount, discount_percent, total_amount, cashier_id) 
                   VALUES (NOW(), %s, %s, %s, %s, %s)""",
                (subtotal, tax_amount, discount_percent, total_amount, cashier_id)
            )
            transaction_id = cursor.lastrowid

            for it in items:
                cursor.execute(
                    """INSERT INTO sales_transaction_items 
                       (transaction_id, product_id, quantity, unit_price, line_total) 
                       VALUES (%s, %s, %s, %s, %s)""",
                    (transaction_id, it["product_id"], it["quantity"], it["unit_price"], it["line_total"])
                )

            conn.commit()
            cursor.close()
            conn.close()
            return {"success": True, "data": {"transaction_id": transaction_id}}
        except Exception as ex:
            if conn:
                conn.rollback()
                conn.close()
            return {"success": False, "error": f"MySQL Transaction Recording Error: {str(ex)}"}

    # Mock Fallback
    tx_id = len(MOCK_SALES_TRANSACTIONS) + 1
    MOCK_SALES_TRANSACTIONS.append({
        "transaction_id": tx_id,
        "date": datetime.datetime.now().isoformat(),
        "subtotal": subtotal,
        "tax_amount": tax_amount,
        "discount_percent": discount_percent,
        "total_amount": total_amount,
        "cashier_id": cashier_id,
        "items": items
    })
    return {"success": True, "data": {"transaction_id": tx_id}}


def record_stock_arrival(params: Dict[str, Any]) -> Dict[str, Any]:
    """
    Logs newly delivered stock batch into PostgreSQL (store_inventory).
    Restocking workflow: Product ID, Quantity, Delivery Date, Expiry Date.
    """
    product_id = params.get("product_id")
    quantity = params.get("quantity")
    delivery_date = params.get("delivery_date")
    expiry_date = params.get("expiry_date")

    if not product_id or quantity is None or not delivery_date or not expiry_date:
        return {"success": False, "error": "Missing required fields for stock arrival."}

    try:
        quantity = int(quantity)
        if quantity <= 0:
            return {"success": False, "error": "Quantity must be greater than zero."}
    except ValueError:
        return {"success": False, "error": "Invalid quantity."}

    conn = get_postgres_connection()
    if conn:
        try:
            cursor = conn.cursor()
            cursor.execute(
                """INSERT INTO inventory (product_id, quantity, delivery_date, expiry_date) 
                   VALUES (%s, %s, %s, %s) RETURNING batch_id""",
                (product_id, quantity, delivery_date, expiry_date)
            )
            batch_id = cursor.fetchone()[0]
            conn.commit()
            cursor.close()
            conn.close()
            return {"success": True, "data": {"batch_id": batch_id, "message": "Stock arrival recorded."}}
        except Exception as ex:
            if conn:
                conn.rollback()
                conn.close()
            return {"success": False, "error": f"PostgreSQL Stock Arrival Error: {str(ex)}"}

    # Mock Fallback
    new_batch_id = len(MOCK_INVENTORY_BATCHES) + 1
    MOCK_INVENTORY_BATCHES.append({
        "batch_id": new_batch_id,
        "product_id": int(product_id),
        "quantity": int(quantity),
        "delivery_date": str(delivery_date),
        "expiry_date": str(expiry_date)
    })
    return {"success": True, "data": {"batch_id": new_batch_id, "message": "Stock arrival recorded."}}


def get_inventory(params: Optional[Dict[str, Any]] = None) -> Dict[str, Any]:
    """
    Retrieves full inventory batch listing joined with Product master data.
    """
    products_res = get_all_products()
    products_map = {p["product_id"]: p for p in products_res.get("data", [])}

    conn = get_postgres_connection()
    if conn:
        try:
            cursor = conn.cursor(cursor_factory=RealDictCursor)
            cursor.execute(
                """SELECT batch_id, product_id, quantity, 
                          to_char(delivery_date, 'YYYY-MM-DD') as delivery_date, 
                          to_char(expiry_date, 'YYYY-MM-DD') as expiry_date 
                   FROM inventory ORDER BY product_id ASC, delivery_date ASC"""
            )
            rows = cursor.fetchall()
            cursor.close()
            conn.close()

            result = []
            for r in rows:
                p = products_map.get(r["product_id"], {})
                result.append({
                    "batch_id": r["batch_id"],
                    "product_id": r["product_id"],
                    "barcode": p.get("barcode", "N/A"),
                    "name": p.get("name", f"Product #{r['product_id']}"),
                    "quantity": r["quantity"],
                    "delivery_date": r["delivery_date"],
                    "expiry_date": r["expiry_date"]
                })
            return {"success": True, "data": result}
        except Exception as ex:
            return {"success": False, "error": f"PostgreSQL Inventory Query Error: {str(ex)}"}

    # Mock Fallback
    result = []
    for b in sorted(MOCK_INVENTORY_BATCHES, key=lambda x: (x["product_id"], x["delivery_date"])):
        p = products_map.get(b["product_id"], {})
        result.append({
            "batch_id": b["batch_id"],
            "product_id": b["product_id"],
            "barcode": p.get("barcode", "N/A"),
            "name": p.get("name", f"Product #{b['product_id']}"),
            "quantity": b["quantity"],
            "delivery_date": str(b["delivery_date"]),
            "expiry_date": str(b["expiry_date"])
        })
    return {"success": True, "data": result}


def get_low_stock(params: Optional[Dict[str, Any]] = None) -> Dict[str, Any]:
    """
    Calculates aggregated stock per product and returns items whose total
    quantity falls below the LOW_STOCK_THRESHOLD.
    """
    threshold = params.get("threshold", LOW_STOCK_THRESHOLD) if params else LOW_STOCK_THRESHOLD
    products_res = get_all_products()
    products_map = {p["product_id"]: p for p in products_res.get("data", [])}

    conn = get_postgres_connection()
    if conn:
        try:
            cursor = conn.cursor(cursor_factory=RealDictCursor)
            cursor.execute(
                """SELECT product_id, SUM(quantity) as total_quantity 
                   FROM inventory 
                   GROUP BY product_id 
                   HAVING SUM(quantity) < %s 
                   ORDER BY total_quantity ASC""",
                (threshold,)
            )
            rows = cursor.fetchall()
            cursor.close()
            conn.close()

            results = []
            for r in rows:
                p = products_map.get(r["product_id"], {})
                results.append({
                    "product_id": r["product_id"],
                    "barcode": p.get("barcode", "N/A"),
                    "name": p.get("name", f"Product #{r['product_id']}"),
                    "total_quantity": int(r["total_quantity"]),
                    "threshold": threshold
                })
            return {"success": True, "data": results}
        except Exception as ex:
            return {"success": False, "error": f"PostgreSQL Low Stock Error: {str(ex)}"}

    # Mock Fallback
    totals: Dict[int, int] = {}
    for b in MOCK_INVENTORY_BATCHES:
        pid = b["product_id"]
        totals[pid] = totals.get(pid, 0) + b["quantity"]

    results = []
    for pid, prod in products_map.items():
        qty = totals.get(pid, 0)
        if qty < threshold:
            results.append({
                "product_id": pid,
                "barcode": prod.get("barcode", "N/A"),
                "name": prod.get("name", f"Product #{pid}"),
                "total_quantity": qty,
                "threshold": threshold
            })
    results.sort(key=lambda x: x["total_quantity"])
    return {"success": True, "data": results}


def get_expiring_soon(params: Optional[Dict[str, Any]] = None) -> Dict[str, Any]:
    """
    Returns all batches with quantity > 0 expiring within EXPIRY_WARNING_DAYS.
    """
    days = params.get("days", EXPIRY_WARNING_DAYS) if params else EXPIRY_WARNING_DAYS
    products_res = get_all_products()
    products_map = {p["product_id"]: p for p in products_res.get("data", [])}

    conn = get_postgres_connection()
    if conn:
        try:
            cursor = conn.cursor(cursor_factory=RealDictCursor)
            cursor.execute(
                """SELECT batch_id, product_id, quantity,
                          to_char(delivery_date, 'YYYY-MM-DD') as delivery_date,
                          to_char(expiry_date, 'YYYY-MM-DD') as expiry_date,
                          (expiry_date - CURRENT_DATE) as days_remaining
                   FROM inventory
                   WHERE quantity > 0 AND expiry_date <= (CURRENT_DATE + %s * INTERVAL '1 day')
                   ORDER BY expiry_date ASC""",
                (days,)
            )
            rows = cursor.fetchall()
            cursor.close()
            conn.close()

            results = []
            for r in rows:
                p = products_map.get(r["product_id"], {})
                results.append({
                    "batch_id": r["batch_id"],
                    "product_id": r["product_id"],
                    "name": p.get("name", f"Product #{r['product_id']}"),
                    "barcode": p.get("barcode", "N/A"),
                    "quantity": r["quantity"],
                    "delivery_date": r["delivery_date"],
                    "expiry_date": r["expiry_date"],
                    "days_remaining": int(r["days_remaining"])
                })
            return {"success": True, "data": results}
        except Exception as ex:
            return {"success": False, "error": f"PostgreSQL Expiry Query Error: {str(ex)}"}

    # Mock Fallback
    results = []
    for b in MOCK_INVENTORY_BATCHES:
        if b["quantity"] <= 0:
            continue
        exp_date = datetime.date.fromisoformat(b["expiry_date"])
        diff_days = (exp_date - today).days
        if diff_days <= days:
            p = products_map.get(b["product_id"], {})
            results.append({
                "batch_id": b["batch_id"],
                "product_id": b["product_id"],
                "name": p.get("name", f"Product #{b['product_id']}"),
                "barcode": p.get("barcode", "N/A"),
                "quantity": b["quantity"],
                "delivery_date": b["delivery_date"],
                "expiry_date": b["expiry_date"],
                "days_remaining": diff_days
            })
    results.sort(key=lambda x: x["days_remaining"])
    return {"success": True, "data": results}


# ==============================================================================
# CLI and Dispatch Router (JSON IPC)
# ==============================================================================
ACTION_DISPATCH = {
    "authenticate_user": authenticate_user,
    "get_all_products": get_all_products,
    "lookup_product": lookup_product,
    "update_product_price": update_product_price,
    "record_sale": record_sale,
    "record_stock_arrival": record_stock_arrival,
    "get_inventory": get_inventory,
    "get_low_stock": get_low_stock,
    "get_expiring_soon": get_expiring_soon
}


def dispatch(payload: Dict[str, Any]) -> Dict[str, Any]:
    action = payload.get("action")
    params = payload.get("params", {})

    handler = ACTION_DISPATCH.get(action)
    if not handler:
        return {"success": False, "error": f"Unknown action: '{action}'"}

    try:
        return handler(params)
    except Exception as ex:
        return {"success": False, "error": f"Exception handling '{action}': {str(ex)}"}


def main():
    """
    Accepts JSON payload either via CLI argument or standard input (stdin).
    Emits single-line JSON response to stdout.
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
