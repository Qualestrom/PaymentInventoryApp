-- ==============================================================================
-- Automated Payment and Inventory Management System
-- Data Tier: PostgreSQL Database Schema (store_inventory)
-- Inventory Batches with FIFO Queue Support and Expiry Tracking
-- ==============================================================================

-- If connecting to a fresh postgres instance, create database:
-- CREATE DATABASE store_inventory;
-- \c store_inventory;

DROP TABLE IF EXISTS inventory CASCADE;

-- 1. Batch Inventory Table
CREATE TABLE inventory (
    batch_id SERIAL PRIMARY KEY,
    product_id INT NOT NULL,
    quantity INT NOT NULL CHECK (quantity >= 0),
    delivery_date DATE NOT NULL,
    expiry_date DATE NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 2. Index Optimization for FIFO Deduction Queries (oldest delivery_date first)
CREATE INDEX idx_inventory_fifo ON inventory (product_id, delivery_date ASC);

-- 3. Index for Expiry Date Queries
CREATE INDEX idx_inventory_expiry ON inventory (expiry_date ASC);

-- ==============================================================================
-- SEED DATA (3 Batches per Product = 15 Batches)
-- Batches configured with realistic delivery dates and expiry dates:
-- Note: Some batches are configured near CURRENT_DATE for Smart Expiry Alerts testing.
-- ==============================================================================

INSERT INTO inventory (product_id, quantity, delivery_date, expiry_date) VALUES
-- Product 1: Whole Milk 1L (Perishable - near expiry batch included)
(1, 15, CURRENT_DATE - INTERVAL '10 days', CURRENT_DATE + INTERVAL '2 days'),  -- EXPIRES IN 2 DAYS (Triggers Smart Alert)
(1, 25, CURRENT_DATE - INTERVAL '5 days',  CURRENT_DATE + INTERVAL '12 days'),
(1, 40, CURRENT_DATE - INTERVAL '1 day',   CURRENT_DATE + INTERVAL '20 days'),

-- Product 2: White Bread Loaf (Short shelf-life - near expiry batch included)
(2, 10, CURRENT_DATE - INTERVAL '4 days',  CURRENT_DATE + INTERVAL '3 days'),  -- EXPIRES IN 3 DAYS (Triggers Smart Alert)
(2, 20, CURRENT_DATE - INTERVAL '2 days',  CURRENT_DATE + INTERVAL '6 days'),  -- EXPIRES IN 6 DAYS (Triggers Smart Alert)
(2, 30, CURRENT_DATE,                      CURRENT_DATE + INTERVAL '10 days'),

-- Product 3: Canned Tuna 155g (Long shelf-life)
(3, 50, CURRENT_DATE - INTERVAL '60 days', CURRENT_DATE + INTERVAL '500 days'),
(3, 40, CURRENT_DATE - INTERVAL '30 days', CURRENT_DATE + INTERVAL '530 days'),
(3, 60, CURRENT_DATE - INTERVAL '5 days',  CURRENT_DATE + INTERVAL '600 days'),

-- Product 4: Instant Coffee 200g (Low stock scenario for testing Manager Alerts)
(4, 3,  CURRENT_DATE - INTERVAL '90 days', CURRENT_DATE + INTERVAL '300 days'),  -- Low quantity
(4, 4,  CURRENT_DATE - INTERVAL '45 days', CURRENT_DATE + INTERVAL '360 days'),  -- Total stock = 7 (< 10 low-stock threshold)

-- Product 5: Bottled Water 500mL (Stable stock)
(5, 50, CURRENT_DATE - INTERVAL '20 days', CURRENT_DATE + INTERVAL '365 days'),
(5, 50, CURRENT_DATE - INTERVAL '10 days', CURRENT_DATE + INTERVAL '375 days'),
(5, 100, CURRENT_DATE - INTERVAL '1 day',  CURRENT_DATE + INTERVAL '390 days');
