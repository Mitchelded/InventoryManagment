namespace InventoryManagment.Models.Entities;

public class Role
{
    public int RoleID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public virtual List<UserRole> UserRoles { get; set; } = new();
}