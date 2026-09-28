# ==============================================================================
# Automated Payment and Inventory Management System
# Backend Unit Test Suite (test_backend.py)
# Verifies FIFO Deductions, Expiry Alerts, Tax/Discount Math, RBAC & Security
# ==============================================================================

import unittest
import copy
import datetime
import db_bridge
import inventory_engine


class TestBackendLogic(unittest.TestCase):

    def setUp(self):
        # Save snapshot of mock databases to preserve isolation across tests
        self._orig_users = copy.deepcopy(db_bridge.MOCK_USERS)
        self._orig_products = copy.deepcopy(db_bridge.MOCK_PRODUCTS)
        self._orig_batches = copy.deepcopy(db_bridge.MOCK_INVENTORY_BATCHES)

    def tearDown(self):
        # Restore original state
        db_bridge.MOCK_USERS = self._orig_users
        db_bridge.MOCK_PRODUCTS = self._orig_products
        db_bridge.MOCK_INVENTORY_BATCHES = self._orig_batches

    # 1. RBAC Authentication Tests
    def test_authenticate_valid_manager(self):
        res = db_bridge.authenticate_user({"username": "admin", "password": "admin123"})
        self.assertTrue(res["success"])
        self.assertEqual(res["data"]["role"], "Manager")
        self.assertEqual(res["data"]["username"], "admin")

    def test_authenticate_valid_cashier(self):
        res = db_bridge.authenticate_user({"username": "cashier1", "password": "cashier123"})
        self.assertTrue(res["success"])
        self.assertEqual(res["data"]["role"], "Cashier")

    def test_authenticate_invalid_password(self):
        res = db_bridge.authenticate_user({"username": "admin", "password": "wrongpassword"})
        self.assertFalse(res["success"])
        self.assertIn("Invalid", res["error"])

    # 2. Manager Price Update & Security Tests
    def test_price_update_manager_allowed(self):
        res = db_bridge.update_product_price({
            "product_id": 1,
            "new_price": 99.50,
            "role": "Manager"
        })
        self.assertTrue(res["success"])
        # Verify updated price in product lookup
        lookup = db_bridge.lookup_product({"product_id": 1})
        self.assertEqual(lookup["data"]["price"], 99.50)

    def test_price_update_cashier_rejected(self):
        res = db_bridge.update_product_price({
            "product_id": 1,
            "new_price": 49.00,
            "role": "Cashier"
        })
        self.assertFalse(res["success"])
        self.assertIn("Authorization denied", res["error"])

    # 3. Smart Expiry Alerts Tests
    def test_smart_alert_near_expiry(self):
        # Product 1 (Milk) has a batch expiring in 2 days
        res = inventory_engine.lookup_product_with_smart_alert({"barcode": "4901234567890"})
        self.assertTrue(res["success"])
        self.assertTrue(res["data"]["is_near_expiry"])
        self.assertIsNotNone(res["data"]["days_until_expiry"])
        self.assertLessEqual(res["data"]["days_until_expiry"], 7)

    def test_smart_alert_not_near_expiry(self):
        # Product 5 (Water) batches expire in 365+ days
        res = inventory_engine.lookup_product_with_smart_alert({"barcode": "4905678901234"})
        self.assertTrue(res["success"])
        self.assertFalse(res["data"]["is_near_expiry"])
        self.assertIsNone(res["data"]["days_until_expiry"])

    # 4. Tax and Discount Math Tests
    def test_calculate_transaction_no_discount(self):
        # 2 Milk (89.75 each) = 179.50. Tax at 12% = 21.54. Total = 201.04
        items = [{"product_id": 1, "quantity": 2, "unit_price": 89.75}]
        res = inventory_engine.calculate_transaction({
            "items": items,
            "tax_rate": 0.12,
            "discount_percent": 0.0
        })
        self.assertTrue(res["success"])
        data = res["data"]
        self.assertEqual(data["subtotal"], 179.50)
        self.assertEqual(data["discount_amount"], 0.0)
        self.assertEqual(data["taxable_amount"], 179.50)
        self.assertEqual(data["tax_amount"], 21.54)
        self.assertEqual(data["total"], 201.04)

    def test_calculate_transaction_with_20_percent_discount(self):
        # Subtotal: 100.00. 20% discount: 20.00. Taxable: 80.00. 12% Tax: 9.60. Total: 89.60
        items = [{"product_id": 99, "quantity": 1, "unit_price": 100.00}]
        res = inventory_engine.calculate_transaction({
            "items": items,
            "tax_rate": 0.12,
            "discount_percent": 20.0
        })
        self.assertTrue(res["success"])
        data = res["data"]
        self.assertEqual(data["subtotal"], 100.00)
        self.assertEqual(data["discount_amount"], 20.00)
        self.assertEqual(data["taxable_amount"], 80.00)
        self.assertEqual(data["tax_amount"], 9.60)
        self.assertEqual(data["total"], 89.60)

    # 5. FIFO Stock Deduction Tests
    def test_fifo_deduction_order(self):
        # Set up 2 controlled batches for test product 1:
        # Batch A: delivery 10 days ago, qty 10
        # Batch B: delivery 2 days ago, qty 20
        d_old = (datetime.date.today() - datetime.timedelta(days=10)).isoformat()
        d_new = (datetime.date.today() - datetime.timedelta(days=2)).isoformat()
        exp = (datetime.date.today() + datetime.timedelta(days=30)).isoformat()

        db_bridge.MOCK_INVENTORY_BATCHES = [
            {"batch_id": 101, "product_id": 1, "quantity": 10, "delivery_date": d_old, "expiry_date": exp},
            {"batch_id": 102, "product_id": 1, "quantity": 20, "delivery_date": d_new, "expiry_date": exp}
        ]

        # Request 15 units.
        # FIFO must deplete all 10 from Batch 101 (oldest), and 5 from Batch 102.
        res = inventory_engine.deduct_stock_fifo({
            "items": [{"product_id": 1, "quantity": 15}]
        })
        self.assertTrue(res["success"])

        b101 = next(b for b in db_bridge.MOCK_INVENTORY_BATCHES if b["batch_id"] == 101)
        b102 = next(b for b in db_bridge.MOCK_INVENTORY_BATCHES if b["batch_id"] == 102)

        self.assertEqual(b101["quantity"], 0, "Oldest batch must be completely consumed first.")
        self.assertEqual(b102["quantity"], 15, "Newer batch must have remaining 15 units.")

    def test_fifo_insufficient_stock(self):
        # Only 7 units of Product 4 in mock store. Request 10 units.
        res = inventory_engine.deduct_stock_fifo({
            "items": [{"product_id": 4, "quantity": 10}]
        })
        self.assertFalse(res["success"])
        self.assertIn("Insufficient stock", res["error"])

    # 6. Stock Intake Restocking Test
    def test_record_stock_arrival(self):
        del_date = datetime.date.today().isoformat()
        exp_date = (datetime.date.today() + datetime.timedelta(days=90)).isoformat()
        res = db_bridge.record_stock_arrival({
            "product_id": 2,
            "quantity": 25,
            "delivery_date": del_date,
            "expiry_date": exp_date
        })
        self.assertTrue(res["success"])
        self.assertIn("batch_id", res["data"])


if __name__ == "__main__":
    unittest.main()
