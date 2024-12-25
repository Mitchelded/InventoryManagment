namespace InventoryManagment.Models.Entities;

public class Status
{
    public int StatusID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual List<Equipment> Equipments { get; set; } = new();
    public virtual List<WarrantyClaim> WarrantyClaims { get; set; } = new();
}