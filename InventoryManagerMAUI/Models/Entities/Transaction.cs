namespace InventoryManagment.Models.Entities;

public class Transaction
{
    public int TransactionID { get; set; }
    public int EquipmentID { get; set; }
    public int WarehouseID { get; set; }
    public int? UserID { get; set; }  // Теперь это может быть User (сотрудник)
    public int Quantity { get; set; }
    public string TransactionType { get; set; }  // "Поступление", "Выдача"
    public DateTime TransactionDate { get; set; }

    public Equipment Equipment { get; set; }
    public Warehouse Warehouse { get; set; }
    public User User { get; set; } // Сотрудник, совершивший транзакцию
}