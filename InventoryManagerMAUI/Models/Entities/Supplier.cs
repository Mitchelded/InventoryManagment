namespace InventoryManagment.Models.Entities;

public class Supplier
{
    public int SupplierID { get; set; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    // Навигационное свойство для связи с Equipment
    public ICollection<Equipment> Equipments { get; set; }
}