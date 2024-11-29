namespace InventoryManagment.Models.Entities;

public class Warehouse
{
    public int WarehouseID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public ICollection<Stock> Stocks { get; set; }
    // Коллекция для учета движения оборудования, поступающего и исходящего со склада
    public ICollection<EquipmentMovement> SourceWarehouseMovements { get; set; }

    // Коллекция движений оборудования для склада назначения
    public ICollection<EquipmentMovement> DestinationWarehouseMovements { get; set; }
}