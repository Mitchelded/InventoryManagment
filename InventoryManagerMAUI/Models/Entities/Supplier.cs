namespace InventoryManagment.Models.Entities;

public class Supplier
{
    public int SupplierID { get; set; }
    public string CompanyName { get; set; }
    public string? ContactInfo { get; set; }
    
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public int CategoryID { get; set; }
    
    
    
    
    public virtual Category Category { get; set; }
    // Навигационное свойство для связи с Equipment
    public virtual List<Equipment> Equipments { get; set; } = new();
}