namespace InventoryManagment.Models.Entities;

public class EquipmentMovement
{
    public int EquipmentMovementID { get; set; }
    public int EquipmentID { get; set; }
    public int SourceWarehouseID { get; set; }  // Склад, с которого было перемещено оборудование
    public int DestinationWarehouseID { get; set; }  // Склад, на который было перемещено оборудование
    public int? UserID { get; set; }  // Сотрудник, который осуществил перемещение
    public DateTime MovementDate { get; set; }
    public string MovementType { get; set; }  // Тип движения, например: "Поступление", "Выдача", "Перемещение"
    public int Quantity { get; set; }  // Количество перемещаемого оборудования
    public string Notes { get; set; }  // Примечания

    public virtual Equipment Equipment { get; set; }
    public virtual Warehouse SourceWarehouse { get; set; }
    public virtual Warehouse DestinationWarehouse { get; set; }
    public virtual User User { get; set; }
}