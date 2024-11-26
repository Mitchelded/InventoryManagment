namespace InventoryManagment.Models.Entities;

public class User
{
    [Key]
    public int IdUser { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Username { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    public DateTime? LastLogin { get; set; }
    
    [Required]
    public bool IsActive { get; set; }
    
    public ICollection<Employee> Employees { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}