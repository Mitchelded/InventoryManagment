namespace InventoryManagment.Models.Entities;

public class OrderDetail
{
    public int OrderDetailID { get; set; }
    public int OrderID { get; set; }
    public int EquipmentID { get; set; }
    public int Quantity { get; set; }

    public decimal? Cost => Equipment != null ? Quantity * Equipment.Cost : 0;

    public string? Notes { get; set; }

    public virtual Order Order { get; set; }
    public virtual Equipment Equipment { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}