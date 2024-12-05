namespace InventoryManagment.Models.Entities;

public class UserRole
{
    public int UserRoleID { get; set; }
    public int UserID { get; set; }
    public int RoleID { get; set; }

    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}