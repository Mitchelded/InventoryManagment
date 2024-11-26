namespace InventoryManagment.Models.Entities;

public class Warehouse
{
    [Key]
    public int IdWarehouse { get; set; }
    
    [Required]
    public string WarehouseName { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public int Capacity { get; set; }
    
    public string Description { get; set; }
    
    public ICollection<Location> Locations { get; set; }
    public ICollection<Equipment> Equipments { get; set; }
    public ICollection<Movement> FromMovements { get; set; }
    public ICollection<Movement> ToMovements { get; set; }
}