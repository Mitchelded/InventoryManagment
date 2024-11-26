namespace InventoryManagment.Models.Entities;

public class Location
{
    [Key]
    public int IdLocation { get; set; }
    
    [Required]
    public string LocationName { get; set; }
    
    public string? LocationDescription { get; set; }
    
    [ForeignKey("Warehouse")]
    public int WarehouseID { get; set; }
    
    public Warehouse Warehouse { get; set; }
}