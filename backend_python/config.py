# ==============================================================================
# Automated Payment and Inventory Management System
# Application Logic Tier: Configuration File
# ==============================================================================
import os

# MySQL Database Settings (store_sales)
MYSQL_CONFIG = {
    "host": os.environ.get("MYSQL_HOST", "localhost"),
    "port": int(os.environ.get("MYSQL_PORT", 3306)),
    "user": os.environ.get("MYSQL_USER", "root"),
    "password": os.environ.get("MYSQL_PASSWORD", ""),
    "database": os.environ.get("MYSQL_DB", "store_sales"),
    "connect_timeout": 3
}

# PostgreSQL Database Settings (store_inventory)
POSTGRES_CONFIG = {
    "host": os.environ.get("PG_HOST", "localhost"),
    "port": int(os.environ.get("PG_PORT", 5432)),
    "user": os.environ.get("PG_USER", "postgres"),
    "password": os.environ.get("PG_PASSWORD", "postgres"),
    "database": os.environ.get("PG_DB", "store_inventory"),
    "connect_timeout": 3
}

# Business Rules and System Thresholds
LOW_STOCK_THRESHOLD = int(os.environ.get("LOW_STOCK_THRESHOLD", 10))        # Units
EXPIRY_WARNING_DAYS = int(os.environ.get("EXPIRY_WARNING_DAYS", 7))         # Days before expiry
DEFAULT_TAX_RATE = float(os.environ.get("DEFAULT_TAX_RATE", 0.12))          # 12% Value-Added Tax (VAT)
