namespace PaymentInventoryApp.Models
{
    /// <summary>
    /// Represents an authenticated user in the system with Role-Based Access Control (RBAC).
    /// Roles: 'Manager' or 'Cashier'.
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "Cashier" or "Manager"
        public string FullName { get; set; } = string.Empty;

        public bool IsManager => string.Equals(Role, "Manager", StringComparison.OrdinalIgnoreCase);
        public bool IsCashier => string.Equals(Role, "Cashier", StringComparison.OrdinalIgnoreCase);
    }
}
