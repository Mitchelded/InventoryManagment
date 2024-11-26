namespace InventoryManagment.Models.Entities;

public class EmployeeAssignment
{
    [Key]
    public int IdAssignment { get; set; }
    
    [ForeignKey("Employee")]
    public int EmployeeID { get; set; }
    
    [ForeignKey("Equipment")]
    public int EquipmentID { get; set; }
    
    [Required]
    public DateTime AssignmentDate { get; set; }
    
    public DateTime? ReturnDate { get; set; }
    
    public Employee Employee { get; set; }
    public Equipment Equipment { get; set; }
}