namespace InventoryManagment.Models.Entities;

public class Stock
{
    public int StockID { get; set; }
    public int EquipmentID { get; set; }
    public int WarehouseID { get; set; }
    public int Quantity { get; set; }

    public Equipment Equipment { get; set; }
    public Warehouse Warehouse { get; set; }
}