namespace InventoryManagment.Models.Entities;

public class Model
{
    [Key]
    public int IdModel { get; set; }
    
    [Required]
    public string ModelName { get; set; }
    
    public ICollection<Equipment> Equipments { get; set; }
}