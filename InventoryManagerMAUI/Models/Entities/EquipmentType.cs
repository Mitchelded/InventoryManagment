namespace InventoryManagment.Models.Entities;

public class EquipmentType
{
    [Key]
    public int IdEquipmentType { get; set; }
    
    [Required]
    public string TypeName { get; set; }
    
    public ICollection<Equipment> Equipments { get; set; }
}