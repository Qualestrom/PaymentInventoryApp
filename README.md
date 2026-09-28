# Automated Payment and Inventory Management Desktop Application

Three-tier retail point-of-sale and inventory control system strictly engineered in accordance with undergraduate engineering thesis specifications (Chapters 1–3).

---

## 1. System Architecture

```
                    ┌──────────────────────────────────────────────┐
                    │      UI Tier (.NET 8 Windows Forms)          │
                    │   • LoginForm (Authentication & RBAC)        │
                    │   • StaffPOSDashboard (Barcode, Smart Alert, │
                    │       Tax/Discount, Thermal PDF Receipt)     │
                    │   • ManagerInventoryDashboard (PostgreSQL    │
                    │       Batches, Alerts, Restocking, Pricing)  │
                    └──────────────────────┬───────────────────────┘
                                           │ System.Diagnostics.Process
                                           │ (JSON stdin/stdout IPC)
                                           ▼
                    ┌──────────────────────────────────────────────┐
                    │       Application Logic Tier (Python 3)      │
                    │   • inventory_engine.py (FIFO deduction,     │
                    │       Smart Expiry alerts, Tax/Discount math)│
                    │   • db_bridge.py (Query dispatcher & RBAC)   │
                    │   • config.py (Connection configs & limits)  │
                    └──────────────┬────────────────┬──────────────┘
                                   │                │
                                   ▼                ▼
                    ┌─────────────────────┐  ┌─────────────────────┐
                    │ MySQL (store_sales) │  │ PostgreSQL          │
                    │ • users             │  │   (store_inventory) │
                    │ • products          │  │ • inventory         │
                    │ • sales_trans...    │  │   (FIFO index on    │
                    │ • sales_trans_items │  │    delivery_date)   │
                    └─────────────────────┘  └─────────────────────┘
```

---

## 2. Directory Structure

```
PaymentInventoryApp/
├── database/
│   ├── mysql_schema.sql          # MySQL tables (users, products, sales) + seed data
│   └── postgres_schema.sql       # PostgreSQL batch inventory with FIFO index
├── backend_python/
│   ├── config.py                 # DB configs, timeouts, thresholds
│   ├── db_bridge.py              # Query router, RBAC verification, DB handlers
│   ├── inventory_engine.py       # FIFO stock deduction, smart alerts, tax calculations
│   └── test_backend.py           # Comprehensive unittest suite (12 test cases)
└── frontend_csharp/
    └── PaymentInventoryApp/
        ├── PaymentInventoryApp.sln
        ├── PaymentInventoryApp.csproj
        ├── Program.cs            # Entry point launching LoginForm
        ├── Forms/
        │   ├── LoginForm.cs (.Designer.cs)               # User authentication & RBAC
        │   ├── MainForm.cs (.Designer.cs)                # Role-gated tab container
        │   ├── StaffPOSDashboard.cs (.Designer.cs)       # POS checkout & smart alerts
        │   └── ManagerInventoryDashboard.cs (.Designer.cs) # Stock control & price updates
        ├── Models/
        │   ├── User.cs                                  # RBAC credentials
        │   ├── Product.cs                               # Product catalog
        │   ├── CartItem.cs                              # Cart & alert indicators
        │   ├── InventoryBatch.cs                        # PostgreSQL batch item
        │   └── SaleTransaction.cs                       # Transaction record
        └── Services/
            ├── PythonBridge.cs                          # Subprocess IPC handler
            └── PdfReceiptService.cs                     # Thermal printer PDF simulator
```

---

## 3. Seed Accounts & Role-Based Access Control (RBAC)

| Username | Password | Role | Permitted Access |
|---|---|---|---|
| `admin` | `admin123` | **Manager** | Full clearance: Staff POS Checkout, Inventory Batch Surveillance, Stock Intake / Restocking, MySQL Product Price Management. |
| `cashier1` | `cashier123` | **Cashier** | Restricted clearance: Staff POS Checkout only. Manager Inventory Dashboard is locked/hidden. |

---

## 4. Hardware Simulation Capabilities

1. **Barcode Scanner Simulation:**
   - Cashiers can type or paste barcodes directly into the auto-focused scan field and press `Enter`.
   - Clicking **"⚡ Sample Barcode (Simulate Scan)"** cycles through real mock barcodes (e.g. `4901234567890` Milk, `4902345678901` Bread, `4903456789012` Tuna, etc.) simulating an optical scanner read.
   - **Smart Expiry Alert:** If the scanned item's earliest active batch in PostgreSQL expires within 7 days, the row is dynamically styled in **Warning Amber/Orange** with warning audio and days-remaining notice.

2. **Thermal Receipt Printer Simulation:**
   - Clicking **"Confirm Checkout & Print PDF"** automatically routes formatted thermal receipt output through `System.Drawing.Printing.PrintDocument` targeting the Windows **Microsoft Print to PDF** printer driver.
   - Saves a styled 80mm thermal receipt PDF directly to disk.

---

## 5. Running the Application

### A. Run Backend Unit Tests
```bash
cd backend_python
python test_backend.py
```

### B. Launch the Desktop WinForms Application
```bash
cd frontend_csharp/PaymentInventoryApp
dotnet run
```
*(Or launch the pre-compiled executable at `frontend_csharp/PaymentInventoryApp/bin/Debug/net8.0-windows/PaymentInventoryApp.exe`)*

### C. Database Setup (Optional Live Server Connection)
By default, the backend includes an **intelligent in-memory mock engine with socket probing**: if local MySQL and PostgreSQL instances are offline, it transparently falls back to initialized seed data for instant testing and academic defense grading.

To connect live database daemons:
1. Import `database/mysql_schema.sql` into MySQL (`store_sales`).
2. Import `database/postgres_schema.sql` into PostgreSQL (`store_inventory`).
3. Set environment variables or update `backend_python/config.py` with your credentials.
