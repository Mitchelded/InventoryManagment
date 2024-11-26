namespace InventoryManagment.Models.Entities;

public class Movement
{
    [Key]
    public int IdMovement { get; set; }
    
    [ForeignKey("Equipment")]
    public int EquipmentID { get; set; }
    
    [ForeignKey("FromWarehouse")]
    public int? FromWarehouseID { get; set; }
    
    [ForeignKey("ToWarehouse")]
    public int? ToWarehouseID { get; set; }
    
    [Required]
    public DateTime MovementDate { get; set; }
    
    public string Reason { get; set; }
    
    public Equipment Equipment { get; set; }
    public Warehouse FromWarehouse { get; set; }
    public Warehouse ToWarehouse { get; set; }
}