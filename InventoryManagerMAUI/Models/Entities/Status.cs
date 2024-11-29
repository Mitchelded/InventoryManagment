namespace InventoryManagment.Models.Entities;

public class Status
{
    public int StatusID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Equipment> Equipments { get; set; }
}