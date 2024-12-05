namespace InventoryManagment.Models.Entities;

public class Warehouse
{
    public int WarehouseID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public virtual List<Stock> Stocks { get; set; } = new();
    // Коллекция для учета движения оборудования, поступающего и исходящего со склада
    public virtual List<EquipmentMovement> SourceWarehouseMovements { get; set; } = new();

    // Коллекция движений оборудования для склада назначения
    public virtual List<EquipmentMovement> DestinationWarehouseMovements { get; set; } = new();
}