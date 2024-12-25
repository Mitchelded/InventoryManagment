namespace InventoryManagment.Models.Entities;
// TODO: Реализовать 
public class Maintenance
{
    public int MaintenanceID { get; set; }
    public int EquipmentID { get; set; }

    public DateTime MaintenanceDate { get; set; }
    public string Notes { get; set; }

    public virtual Equipment Equipment { get; set; }

}