namespace InventoryManagment.Models.Entities;

public class User
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }

    public int? DepartmentID { get; set; }  // Отдел сотрудника
    public string FullName { get; set; }    // Имя сотрудника (если это сотрудник)

    public virtual Department Department { get; set; } // Отдел, к которому относится сотрудник (если применимо)
    // Коллекция для учета использования оборудования пользователем
    public virtual List<UtilizationRecord> UtilizationRecords { get; set; } = new();
    public virtual List<UserRole> UserRoles { get; set; } = new();
    public virtual List<EquipmentMovement> EquipmentMovements { get; set; } = new();
}