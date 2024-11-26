namespace InventoryManagment.Models.Entities;

public class Purchase
{
    [Key]
    public int IdPurchase { get; set; }
    
    [ForeignKey("Supplier")]
    public int SupplierID { get; set; }
    
    [ForeignKey("Equipment")]
    public int EquipmentID { get; set; }
    
    [Required]
    public DateTime PurchaseDate { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    public Supplier Supplier { get; set; }
    public Equipment Equipment { get; set; }
}