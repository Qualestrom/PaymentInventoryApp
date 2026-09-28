-- ==============================================================================
-- Automated Payment and Inventory Management System
-- Data Tier: MySQL Database Schema (store_sales)
-- Master Tables: users, products, sales_transactions, sales_transaction_items
-- ==============================================================================

CREATE DATABASE IF NOT EXISTS store_sales;
USE store_sales;

-- 1. User Authentication & Role-Based Access Control (RBAC) Table
DROP TABLE IF EXISTS sales_transaction_items;
DROP TABLE IF EXISTS sales_transactions;
DROP TABLE IF EXISTS products;
DROP TABLE IF EXISTS users;

CREATE TABLE users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    role ENUM('Cashier', 'Manager') NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 2. Master Products Table
CREATE TABLE products (
    product_id INT AUTO_INCREMENT PRIMARY KEY,
    barcode VARCHAR(20) NOT NULL UNIQUE,
    name VARCHAR(100) NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 3. Sales Transactions Table
CREATE TABLE sales_transactions (
    transaction_id INT AUTO_INCREMENT PRIMARY KEY,
    date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    subtotal DECIMAL(10, 2) NOT NULL,
    tax_amount DECIMAL(10, 2) NOT NULL,
    discount_percent DECIMAL(5, 2) NOT NULL DEFAULT 0.00,
    total_amount DECIMAL(10, 2) NOT NULL,
    cashier_id INT NULL,
    FOREIGN KEY (cashier_id) REFERENCES users(user_id) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 4. Sales Transaction Line Items Table
CREATE TABLE sales_transaction_items (
    item_id INT AUTO_INCREMENT PRIMARY KEY,
    transaction_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    unit_price DECIMAL(10, 2) NOT NULL,
    line_total DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (transaction_id) REFERENCES sales_transactions(transaction_id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ==============================================================================
-- SEED DATA
-- ==============================================================================

-- Seed Users:
-- admin: 'admin123' -> SHA-256 hash: 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9
-- cashier1: 'cashier123' -> SHA-256 hash: c0f9ff85c6a1cf8eb5e07661334c9c43ae0a232f3c75ab3f3458db4f4bfba8cf
INSERT INTO users (username, password_hash, role, full_name) VALUES
('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Manager', 'Administrator'),
('cashier1', 'c0f9ff85c6a1cf8eb5e07661334c9c43ae0a232f3c75ab3f3458db4f4bfba8cf', 'Cashier', 'Juan Dela Cruz');

-- Seed Mock Products:
INSERT INTO products (product_id, barcode, name, price) VALUES
(1, '4901234567890', 'Whole Milk 1L', 89.75),
(2, '4902345678901', 'White Bread Loaf', 65.00),
(3, '4903456789012', 'Canned Tuna 155g', 38.50),
(4, '4904567890123', 'Instant Coffee 200g', 245.00),
(5, '4905678901234', 'Bottled Water 500mL', 15.00);
