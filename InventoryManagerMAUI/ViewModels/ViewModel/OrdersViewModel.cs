using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class OrdersViewModel : ViewModelBase<Orders>
{
    private int _idOrder;
    private DateTime _orderDate;
    private int _supplierId;
    private string _status;
    private decimal? _totalCost;
    private string _orderItems;

    public int IdOrder
    {
        get => _idOrder;
        set
        {
            if (value == _idOrder) return;
            _idOrder = value;
            OnPropertyChanged(nameof(IdOrder));
            OnPropertyChanged(nameof(IdOrder));
        }
    }

    public System.DateTime OrderDate
    {
        get => _orderDate;
        set
        {
            if (value.Equals(_orderDate)) return;
            _orderDate = value;
            OnPropertyChanged(nameof(OrderDate));
            OnPropertyChanged(nameof(OrderDate));
        }
    }

    public int SupplierId
    {
        get => _supplierId;
        set
        {
            if (value == _supplierId) return;
            _supplierId = value;
            OnPropertyChanged(nameof(SupplierId));
            OnPropertyChanged(nameof(SupplierId));
        }
    }

    public string Status
    {
        get => _status;
        set
        {
            if (value == _status) return;
            _status = value;
            OnPropertyChanged(nameof(Status));
            OnPropertyChanged(nameof(Status));
        }
    }

    public decimal? TotalCost
    {
        get => _totalCost;
        set
        {
            if (value == _totalCost) return;
            _totalCost = value;
            OnPropertyChanged(nameof(TotalCost));
        }
    }

    public string OrderItems
    {
        get => _orderItems;
        set
        {
            if (value == _orderItems) return;
            _orderItems = value;
            OnPropertyChanged(nameof(OrderItems));
        }
    }

    public OrdersViewModel()
    {
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities db = new();
        var status = new Orders()
        {
            IdOrder = _idOrder,
            OrderDate = _orderDate,
            SupplierID = _supplierId,
            Status = _status,
            TotalCost = _totalCost,
            OrderItems = _orderItems,
            
        };
        Collection.Add(status);
        db.Add(status);
        db.SaveChanges();
    }
}