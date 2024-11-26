namespace InventoryManagment.Models.Entities;

public class Equipment
{
    [Key]
    public int IdEquipment { get; set; }
    
    [Required]
    public string EquipmentName { get; set; }
    
    [ForeignKey("EquipmentType")]
    public int EquipmentTypeID { get; set; }
    
    [ForeignKey("Brand")]
    public int? BrandID { get; set; }
    
    [ForeignKey("Model")]
    public int? ModelID { get; set; }
    
    [ForeignKey("Condition")]
    public int? ConditionID { get; set; }
    
    [Required]
    public string SerialNumber { get; set; }
    
    [ForeignKey("Warehouse")]
    public int WarehouseID { get; set; }
    
    public EquipmentType EquipmentType { get; set; }
    public Brand Brand { get; set; }
    public Model Model { get; set; }
    public Condition Condition { get; set; }
    public Warehouse Warehouse { get; set; }
    
    public ICollection<Movement> Movements { get; set; }
    public ICollection<EmployeeAssignment> EmployeeAssignments { get; set; }
    public ICollection<Purchase> Purchases { get; set; }
}