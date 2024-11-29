namespace InventoryManagment.Models.Entities;

public class Equipment
{
    public int EquipmentID { get; set; }
    public string Name { get; set; }
    public byte[] Photo { get; set; }
    public int CategoryID { get; set; }
    
    public decimal? Cost { get; set; }
    public string SerialNumber { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public DateTime? WarrantyExpiration { get; set; }
    public int StatusID { get; set; }
    public int SupplierID { get; set; }
    // Навигационное свойство для Supplier
    public Supplier Supplier { get; set; }
    public Category Category { get; set; }
    public Status Status { get; set; }
    // Коллекция для учета движения оборудования
    public ICollection<EquipmentMovement> EquipmentMovements { get; set; }
    // Коллекция для учета использования оборудования пользователем
    public ICollection<UtilizationRecord> UtilizationRecords { get; set; }
}