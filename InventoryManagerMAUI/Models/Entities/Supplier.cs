namespace InventoryManagment.Models.Entities;

public class Supplier
{
    public int SupplierID { get; set; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    // Навигационное свойство для связи с Equipment
    public virtual List<Equipment> Equipments { get; set; } = new();
}