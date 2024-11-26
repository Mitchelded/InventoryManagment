namespace InventoryManagment.Models.Entities;

public class Supplier
{
    [Key]
    public int IdSupplier { get; set; }
    
    [Required]
    public string SupplierName { get; set; }
    
    public string ContactInfo { get; set; }
    
    public ICollection<Purchase> Purchases { get; set; }
}