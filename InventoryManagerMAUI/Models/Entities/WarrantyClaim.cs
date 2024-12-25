namespace InventoryManagment.Models.Entities;
// TODO: Реализовать 
public class WarrantyClaim
{
    public int WarrantyClaimID { get; set; }  // Уникальный идентификатор заявки
    public int EquipmentID { get; set; }  // Идентификатор оборудования, по которому подана заявка
    public DateTime ClaimDate { get; set; }  // Дата подачи заявки
    public string IssueDescription { get; set; }  // Описание проблемы
    public string Resolution { get; set; }  // Решение по заявке
    public DateTime? ResolutionDate { get; set; }  // Дата решения заявки (если применимо)
    public string Status { get; set; }  // Статус заявки (например, "В процессе", "Завершено", "Отклонено")

    // Связь с другими сущностями
    public virtual Equipment Equipment { get; set; }
}