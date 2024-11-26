namespace InventoryManagment.Models.Entities;

public class Brand
{
    [Key]
    public int IdBrand { get; set; }
    
    [Required]
    public string BrandName { get; set; }
    
    public ICollection<Equipment> Equipments { get; set; }
}