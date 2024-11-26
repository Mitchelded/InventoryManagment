namespace InventoryManagment.Models.Entities;

public class UserRole
{
    [Key]
    [Column(Order = 0)]
    public int UserID { get; set; }
    
    [Key]
    [Column(Order = 1)]
    public int RoleID { get; set; }
    
    public User User { get; set; }
    public Role Role { get; set; }
}