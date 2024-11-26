namespace InventoryManagment.Models.Entities;

public class Employee
{
    [Key]
    public int IdEmployee { get; set; }
    
    [ForeignKey("User")]
    public int UserID { get; set; }
    
    [Required]
    public string Position { get; set; }
    
    [Required]
    public string Department { get; set; }
    
    public string Phone { get; set; }
    
    public DateTime HireDate { get; set; }
    
    public User User { get; set; }
    public ICollection<EmployeeAssignment> EmployeeAssignments { get; set; }
}