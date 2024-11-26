namespace InventoryManagment.Models.Entities;

public class Role
{
    [Key]
    public int IdRole { get; set; }
    
    [Required]
    public string RoleName { get; set; }
    
    public string Description { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; }
}