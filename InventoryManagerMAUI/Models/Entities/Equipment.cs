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
    public string LastLocation
    {
        get
        {
            var lastStock = Stocks?.LastOrDefault();
            return lastStock?.Warehouse?.Name ?? "Unknown Location";
        }
    }

    public virtual List<Stock> Stocks { get; set; } = new();
    // Навигационное свойство для Supplier
    public virtual Supplier Supplier { get; set; }
    public virtual Category Category { get; set; }
    public virtual Status Status { get; set; }
    // Коллекция для учета движения оборудования
    public virtual List<EquipmentMovement> EquipmentMovements { get; set; } = new();
    // Коллекция для учета использования оборудования пользователем
    public virtual List<UtilizationRecord> UtilizationRecords { get; set; } = new();
}