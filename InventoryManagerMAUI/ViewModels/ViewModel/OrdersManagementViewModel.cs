using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class OrdersManagementViewModel : ViewModelBase<OrderDetail>
{
    private decimal _totalCost;
    private DateTime _orderDate;
    private string _fullName;
    private int _orderId;

    public async override void LoadData()
    {
        using InventoryManagmentEntities db = new();
        Collection.Clear();
        await db.OrderDetails
            .Include(u => u.Equipment) // Include Supplier navigation property
            .Include(u => u.Order) // Include Stocks navigation property
            .ThenInclude(s => s.User) // Then include Warehouse from Stock navigation property
            .ThenInclude(s=>s.Department)
            
            .LoadAsync(); // Asynchronous load of the query
        foreach (var item in db.OrderDetails.Local)
        {
            Collection.Add(item);
        }
        OnPropertyChanged(nameof(Collection));
    }
    
    public decimal TotalCost
    {
        get => _totalCost;
        set
        {
            if (value == _totalCost) return;
            _totalCost = value;
            OnPropertyChanged(nameof(TotalCost));
        }
    }

    public DateTime OrderDate
    {
        get => _orderDate;
        set
        {
            if (value.Equals(_orderDate)) return;
            _orderDate = value;
            OnPropertyChanged(nameof(OrderDate));
        }
    }

    public string FullName
    {
        get => _fullName;
        set
        {
            if (value == _fullName) return;
            _fullName = value;
            OnPropertyChanged(nameof(FullName));
        }
    } // Имя сотрудника (если это сотрудник)

    public int OrderID
    {
        get => _orderId;
        set
        {
            if (value == _orderId) return;
            _orderId = value;
            OnPropertyChanged(nameof(OrderID));
        }
    }

}