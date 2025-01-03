﻿namespace InventoryManagment.Models.Entities;
// TODO: Удалить
public class UtilizationRecord
{
    public int UtilizationRecordID { get; set; }
    public int EquipmentID { get; set; }  // Идентификатор оборудования
    public int UserID { get; set; }  // Сотрудник, который использовал оборудование
    public DateTime StartDate { get; set; }  // Дата начала использования
    public DateTime? EndDate { get; set; }  // Дата окончания использования (может быть null, если еще используется)
    public string Purpose { get; set; }  // Цель использования
    public string Notes { get; set; }  // Примечания

    // Связи с другими моделями
    public virtual Equipment Equipment { get; set; }
    public virtual User User { get; set; }
}