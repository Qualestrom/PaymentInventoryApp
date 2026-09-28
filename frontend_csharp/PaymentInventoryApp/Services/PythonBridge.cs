using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using PaymentInventoryApp.Models;

namespace PaymentInventoryApp.Services
{
    /// <summary>
    /// Inter-Process Communication (IPC) bridge.
    /// Spawns Python processes (db_bridge.py / inventory_engine.py) using System.Diagnostics.Process,
    /// feeds structured JSON requests via stdin, and parses JSON output from stdout.
    /// </summary>
    public class PythonBridge
    {
        private readonly string _pythonExecutable;
        private readonly string _backendDir;

        public PythonBridge(string? pythonExecutable = null, string? backendDir = null)
        {
            _pythonExecutable = pythonExecutable ?? FindPythonExecutable();
            _backendDir = backendDir ?? FindBackendDirectory();
        }

        private static string FindPythonExecutable()
        {
            // Default to 'python' in PATH
            return "python";
        }

        private static string FindBackendDirectory()
        {
            // Try standard relative paths from executable / solution
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            
            // Look up parent hierarchy for 'backend_python'
            DirectoryInfo? dir = new DirectoryInfo(baseDir);
            for (int i = 0; i < 6 && dir != null; i++)
            {
                string candidate = Path.Combine(dir.FullName, "backend_python");
                if (Directory.Exists(candidate))
                {
                    return candidate;
                }
                dir = dir.Parent;
            }

            // Fallback: check sibling to frontend_csharp
            return Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "..", "backend_python"));
        }

        /// <summary>
        /// Executes a python script with a JSON payload via stdin.
        /// Returns the parsed JSON response object.
        /// </summary>
        public JsonNode ExecuteScript(string scriptName, string action, object? parameters = null)
        {
            string scriptPath = Path.Combine(_backendDir, scriptName);
            if (!File.Exists(scriptPath))
            {
                var errorObj = new JsonObject
                {
                    ["success"] = false,
                    ["error"] = $"Python script not found at path: {scriptPath}"
                };
                return errorObj;
            }

            var requestPayload = new JsonObject
            {
                ["action"] = action,
                ["params"] = parameters != null ? JsonSerializer.SerializeToNode(parameters) : new JsonObject()
            };

            string jsonInput = requestPayload.ToJsonString();

            var startInfo = new ProcessStartInfo
            {
                FileName = _pythonExecutable,
                Arguments = $"\"{scriptPath}\"",
                WorkingDirectory = _backendDir,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using var process = new Process { StartInfo = startInfo };
                process.Start();

                // Send JSON payload to stdin
                using (var writer = process.StandardInput)
                {
                    writer.WriteLine(jsonInput);
                }

                // Read response from stdout
                string stdout = process.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();

                process.WaitForExit(10000);

                if (string.IsNullOrWhiteSpace(stdout))
                {
                    var errNode = new JsonObject
                    {
                        ["success"] = false,
                        ["error"] = string.IsNullOrWhiteSpace(stderr) ? "No output from Python process." : stderr
                    };
                    return errNode;
                }

                var parsed = JsonNode.Parse(stdout.Trim());
                return parsed ?? new JsonObject { ["success"] = false, ["error"] = "Null JSON response." };
            }
            catch (Exception ex)
            {
                var errNode = new JsonObject
                {
                    ["success"] = false,
                    ["error"] = $"IPC Process Execution Error: {ex.Message}"
                };
                return errNode;
            }
        }

        // =====================================================================
        // High-Level Domain Methods
        // =====================================================================

        /// <summary>
        /// Authenticates user credentials against the MySQL database.
        /// </summary>
        public (bool Success, User? User, string? Error) AuthenticateUser(string username, string password)
        {
            var res = ExecuteScript("db_bridge.py", "authenticate_user", new { username, password });
            bool success = res?["success"]?.GetValue<bool>() ?? false;
            if (success && res?["data"] != null)
            {
                var data = res["data"]!;
                var user = new User
                {
                    UserId = data["user_id"]?.GetValue<int>() ?? 0,
                    Username = data["username"]?.GetValue<string>() ?? "",
                    Role = data["role"]?.GetValue<string>() ?? "",
                    FullName = data["full_name"]?.GetValue<string>() ?? ""
                };
                return (true, user, null);
            }

            string error = res?["error"]?.GetValue<string>() ?? "Authentication failed.";
            return (false, null, error);
        }

        /// <summary>
        /// Looks up product by barcode and checks active inventory batches for imminent expiration (Smart Alert).
        /// </summary>
        public (bool Success, Product? Product, string? Error) LookupProduct(string barcode)
        {
            var res = ExecuteScript("inventory_engine.py", "lookup_product", new { barcode });
            bool success = res?["success"]?.GetValue<bool>() ?? false;
            if (success && res?["data"] != null)
            {
                var d = res["data"]!;
                var prod = new Product
                {
                    ProductId = d["product_id"]?.GetValue<int>() ?? 0,
                    Barcode = d["barcode"]?.GetValue<string>() ?? "",
                    Name = d["name"]?.GetValue<string>() ?? "",
                    Price = d["price"]?.GetValue<decimal>() ?? 0m,
                    IsNearExpiry = d["is_near_expiry"]?.GetValue<bool>() ?? false,
                    DaysUntilExpiry = d["days_until_expiry"]?.GetValue<int?>(),
                    NearExpiryBatchId = d["near_expiry_batch_id"]?.GetValue<int?>()
                };
                return (true, prod, null);
            }

            string error = res?["error"]?.GetValue<string>() ?? "Product not found.";
            return (false, null, error);
        }

        /// <summary>
        /// Calculates transaction subtotal, discount, tax (12% VAT), and net total.
        /// </summary>
        public (bool Success, JsonNode? Data, string? Error) CalculateTransaction(
            IEnumerable<CartItem> items, 
            decimal taxRate, 
            decimal discountPercent)
        {
            var payloadItems = items.Select(i => new
            {
                product_id = i.ProductId,
                quantity = i.Quantity,
                unit_price = i.UnitPrice
            }).ToList();

            var res = ExecuteScript("inventory_engine.py", "calculate_transaction", new
            {
                items = payloadItems,
                tax_rate = taxRate,
                discount_percent = discountPercent
            });

            bool success = res?["success"]?.GetValue<bool>() ?? false;
            if (success)
            {
                return (true, res?["data"], null);
            }

            return (false, null, res?["error"]?.GetValue<string>() ?? "Calculation failed.");
        }

        /// <summary>
        /// Executes FIFO inventory deduction against PostgreSQL batches.
        /// </summary>
        public (bool Success, string? Error) DeductStock(IEnumerable<CartItem> items)
        {
            var payloadItems = items.Select(i => new
            {
                product_id = i.ProductId,
                quantity = i.Quantity
            }).ToList();

            var res = ExecuteScript("inventory_engine.py", "deduct_stock", new { items = payloadItems });
            bool success = res?["success"]?.GetValue<bool>() ?? false;
            if (success)
            {
                return (true, null);
            }

            return (false, res?["error"]?.GetValue<string>() ?? "Stock deduction failed.");
        }

        /// <summary>
        /// Records completed sale into MySQL (sales_transactions and line items).
        /// </summary>
        public (bool Success, int TransactionId, string? Error) RecordSale(
            int? cashierId, 
            decimal subtotal, 
            decimal taxAmount, 
            decimal discountPercent, 
            decimal totalAmount, 
            IEnumerable<CartItem> items)
        {
            var payloadItems = items.Select(i => new
            {
                product_id = i.ProductId,
                quantity = i.Quantity,
                unit_price = i.UnitPrice,
                line_total = i.LineTotal
            }).ToList();

            var res = ExecuteScript("db_bridge.py", "record_sale", new
            {
                cashier_id = cashierId,
                subtotal = subtotal,
                tax_amount = taxAmount,
                discount_percent = discountPercent,
                total_amount = totalAmount,
                items = payloadItems
            });

            bool success = res?["success"]?.GetValue<bool>() ?? false;
            if (success)
            {
                int txId = res?["data"]?["transaction_id"]?.GetValue<int>() ?? 0;
                return (true, txId, null);
            }

            return (false, 0, res?["error"]?.GetValue<string>() ?? "Sale recording failed.");
        }

        /// <summary>
        /// Gets all products for dropdowns and catalog views.
        /// </summary>
        public (bool Success, List<Product> Products, string? Error) GetAllProducts()
        {
            var res = ExecuteScript("db_bridge.py", "get_all_products");
            bool success = res?["success"]?.GetValue<bool>() ?? false;
            var list = new List<Product>();

            if (success && res?["data"] is JsonArray arr)
            {
                foreach (var item in arr)
                {
                    if (item == null) continue;
                    list.Add(new Product
                    {
                        ProductId = item["product_id"]?.GetValue<int>() ?? 0,
                        Barcode = item["barcode"]?.GetValue<string>() ?? "",
                        Name = item["name"]?.GetValue<string>() ?? "",
                        Price = item["price"]?.GetValue<decimal>() ?? 0m
                    });
                }
                return (true, list, null);
            }

            return (false, list, res?["error"]?.GetValue<string>() ?? "Failed to fetch products.");
        }

        /// <summary>
        /// Updates product retail price in MySQL. Restricted to role == 'Manager'.
        /// </summary>
        public (bool Success, string Message, string? Error) UpdateProductPrice(int productId, decimal newPrice, string role)
        {
            var res = ExecuteScript("db_bridge.py", "update_product_price", new
            {
                product_id = productId,
                new_price = newPrice,
                role = role
            });

            bool success = res?["success"]?.GetValue<bool>() ?? false;
            if (success)
            {
                string msg = res?["data"]?["message"]?.GetValue<string>() ?? "Price updated successfully.";
                return (true, msg, null);
            }

            return (false, "", res?["error"]?.GetValue<string>() ?? "Price update failed.");
        }

        /// <summary>
        /// Records new supplier stock arrival into PostgreSQL store_inventory.
        /// </summary>
        public (bool Success, int BatchId, string? Error) RecordStockArrival(
            int productId, 
            int quantity, 
            string deliveryDate, 
            string expiryDate)
        {
            var res = ExecuteScript("db_bridge.py", "record_stock_arrival", new
            {
                product_id = productId,
                quantity = quantity,
                delivery_date = deliveryDate,
                expiry_date = expiryDate
            });

            bool success = res?["success"]?.GetValue<bool>() ?? false;
            if (success)
            {
                int batchId = res?["data"]?["batch_id"]?.GetValue<int>() ?? 0;
                return (true, batchId, null);
            }

            return (false, 0, res?["error"]?.GetValue<string>() ?? "Failed to record stock arrival.");
        }

        /// <summary>
        /// Gets full inventory listing from PostgreSQL.
        /// </summary>
        public (bool Success, List<InventoryBatch> Batches, string? Error) GetInventory()
        {
            var res = ExecuteScript("db_bridge.py", "get_inventory");
            bool success = res?["success"]?.GetValue<bool>() ?? false;
            var list = new List<InventoryBatch>();

            if (success && res?["data"] is JsonArray arr)
            {
                foreach (var item in arr)
                {
                    if (item == null) continue;
                    list.Add(new InventoryBatch
                    {
                        BatchId = item["batch_id"]?.GetValue<int>() ?? 0,
                        ProductId = item["product_id"]?.GetValue<int>() ?? 0,
                        Barcode = item["barcode"]?.GetValue<string>() ?? "",
                        ProductName = item["name"]?.GetValue<string>() ?? "",
                        Quantity = item["quantity"]?.GetValue<int>() ?? 0,
                        DeliveryDate = item["delivery_date"]?.GetValue<string>() ?? "",
                        ExpiryDate = item["expiry_date"]?.GetValue<string>() ?? ""
                    });
                }
                return (true, list, null);
            }

            return (false, list, res?["error"]?.GetValue<string>() ?? "Failed to fetch inventory.");
        }

        /// <summary>
        /// Gets low stock items (aggregated quantity < threshold).
        /// </summary>
        public (bool Success, JsonNode? Data, string? Error) GetLowStock(int? threshold = null)
        {
            var res = ExecuteScript("db_bridge.py", "get_low_stock", threshold.HasValue ? new { threshold = threshold.Value } : new object());
            bool success = res?["success"]?.GetValue<bool>() ?? false;
            if (success)
            {
                return (true, res?["data"], null);
            }
            return (false, null, res?["error"]?.GetValue<string>() ?? "Failed to fetch low stock alerts.");
        }

        /// <summary>
        /// Gets expiring batches (expiry_date within warning window).
        /// </summary>
        public (bool Success, JsonNode? Data, string? Error) GetExpiringSoon(int? days = null)
        {
            var res = ExecuteScript("db_bridge.py", "get_expiring_soon", days.HasValue ? new { days = days.Value } : new object());
            bool success = res?["success"]?.GetValue<bool>() ?? false;
            if (success)
            {
                return (true, res?["data"], null);
            }
            return (false, null, res?["error"]?.GetValue<string>() ?? "Failed to fetch expiry alerts.");
        }
    }
}
