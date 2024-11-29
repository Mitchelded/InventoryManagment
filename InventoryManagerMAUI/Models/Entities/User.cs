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

    public Department Department { get; set; } // Отдел, к которому относится сотрудник (если применимо)
    // Коллекция для учета использования оборудования пользователем
    public ICollection<UtilizationRecord> UtilizationRecords { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<EquipmentMovement> EquipmentMovements { get; set; }
}