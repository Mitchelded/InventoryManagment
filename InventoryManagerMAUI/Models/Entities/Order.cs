namespace InventoryManagment.Models.Entities;

public class Order
{
    public int OrderID { get; set; }
    public int UserID { get; set; }
    public DateTime OrderDate { get; set; }
    public string? Notes { get; set; }
    public decimal TotalCost { get; set; }
    
    public string ShippingAddress { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    
    public virtual User User { get; set; }  // Пользователь, создавший заказ
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}