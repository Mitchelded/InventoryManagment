using InventoryManagment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class OrdersManagementViewModel : ViewModelBase<OrderDetail>
{
    private string _shippingAddress;
    private string _notes;

    public string Notes
    {
        get => _notes;
        set
        {
            if (value == _notes) return;
            _notes = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(Notes));
        }
    }

    public string ShippingAddress
    {
        get => _shippingAddress;
        set
        {
            if (value == _shippingAddress) return;
            _shippingAddress = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(ShippingAddress));
        }
    }


    public ObservableCollection<ProductItem> Products { get; set; }
    public List<OrderDetail> OrderDetailsToAdd = new();
    public ICommand AddProductCommand { get; }
    public ICommand DeleteProductCommand { get; }

    private void AddProduct()
    {
        Products.Add(new ProductItem());
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();
        List<Order> orders = new List<Order>();

        var orderToAdd = new Order()
        {
            OrderDate = DateTime.Now,
            ShippingAddress = ShippingAddress,
            CustomerName = CustomerName,
            // TODO: Добавить добавление текущего пользователя кто создал заказ. После добавления логина
            UserID = 1,
            Notes = Notes
        };
        _db.Orders.Add(orderToAdd);
        _db.SaveChanges();
        foreach (var product in Products)
        {
            if (product.Quantity > 0)
            {
                var orderDetail = new OrderDetail()
                {
                    EquipmentID = product.SelectedProduct.EquipmentID,
                    Quantity = product.Quantity,
                    OrderID = orderToAdd.OrderID, // Use the generated ID of the added order
                };

                // Add the order detail to the database
                Collection.Add(orderDetail);
                _db.OrderDetails.Add(orderDetail);
                _db.SaveChanges();
            }
        }
    }

    private void DeleteProduct(ProductItem product)
    {
        if (Products.Contains(product))
            Products.Remove(product);
    }


    private decimal _totalCost;
    private DateTime _orderDate;
    private string _fullName;

    private string _customerName;
    private string _customerEmail;

    private List<Equipment> _equipment = new();

    public List<Equipment> Equipment
    {
        get => _equipment;
        set
        {
            if (Equals(value, _equipment)) return;
            _equipment = value;
            OnPropertyChanged(nameof(Equipment));
        }
    }


    private int _orderId;
    private List<OrderDetail> _orderDetails = new();
    public ICommand DetailCommand { get; }

    private DateTime _startDate;
    private DateTime _endDate;

    private string _filterName;

    public OrdersManagementViewModel()
    {
        DetailCommand = new Command(OrderFilter);
        LoadEquipment();

        Products = new ObservableCollection<ProductItem>();
        AddProductCommand = new Command(AddProduct);
        DeleteProductCommand = new Command<ProductItem>(DeleteProduct);
    }

    private void LoadEquipment()
    {
        using InventoryManagmentEntities _db = new();
        // Load data from the database and populate the ObservableCollection
        var items = _db.Equipments.ToList();
        foreach (var item in items)
        {
            Equipment.Add(item);
        }
    }

    private void ApplyFilter()
    {
        using InventoryManagmentEntities db = new();
        List<OrderDetail> filtered;

        filtered = db.OrderDetails
            .Include(e => e.Equipment)
            .Include(e => e.Order)
            .ThenInclude(e => e.User)
            .Where(e =>
                (e.Order.OrderDate >= StartDate) &&
                (e.Order.OrderDate <= EndDate)
                && (string.IsNullOrEmpty(FilterName) || e.Order.User.FullName.Contains(FilterName)))
            .GroupBy(od => od.OrderID)
            .Select(g => g.First())
            .ToList();

        Collection.Clear();
        foreach (var item in filtered)
            Collection.Add(item);
    }

    public List<OrderDetail> OrderDetails
    {
        get => _orderDetails;
        set
        {
            if (Equals(value, _orderDetails)) return;
            _orderDetails = value;
            OnPropertyChanged(nameof(OrderDetails));
        }
    }

    void OrderFilter()
    {
        if (SelectedItem == null)
        {
            // Handle or log that SelectedItem is null
            return;
        }

        using InventoryManagmentEntities db = new();
        OrderDetails.Clear();
        db.OrderDetails
            .Include(u => u.Equipment) // Include Supplier navigation property
            .Include(u => u.Order) // Include Stocks navigation property
            .ThenInclude(s => s.User) // Then include Warehouse from Stock navigation property
            .ThenInclude(s => s.Department)
            .Where(x => x.OrderID == SelectedItem.OrderID)
            .Load(); // Asynchronous load of the query
        foreach (var item in db.OrderDetails.Local)
        {
            OrderDetails.Add(item);
        }

        OnPropertyChanged(nameof(OrderDetails));
    }


    public async override void LoadData()
    {
        using InventoryManagmentEntities db = new();
        Collection.Clear();
        await db.OrderDetails
            .Include(u => u.Equipment) // Include Supplier navigation property
            .Include(u => u.Order) // Include Stocks navigation property
            .ThenInclude(s => s.User) // Then include Warehouse from Stock navigation property
            .ThenInclude(s => s.Department)
            .GroupBy(od => od.OrderID)
            .Select(g => g.First())
            .LoadAsync(); // Asynchronous load of the query
        foreach (var item in db.OrderDetails.Local)
        {
            Collection.Add(item);
        }

        OnPropertyChanged(nameof(Collection));
    }

    public string CustomerName
    {
        get => _customerName;
        set
        {
            if (value == _customerName) return;
            _customerName = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(CustomerName));
        }
    }

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (Equals(value, _startDate)) return;
            _startDate = value;
            OnPropertyChanged(nameof(StartDate));
            ApplyFilter();
        }
    }

    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            if (Equals(value, _endDate)) return;
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
            ApplyFilter();
        }
    }

    public string CustomerEmail
    {
        get => _customerEmail;
        set
        {
            if (value == _customerEmail) return;
            _customerEmail = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(CustomerEmail));
        }
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


    public string FilterName
    {
        get => _filterName;
        set
        {
            if (value == _filterName) return;
            _filterName = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(FilterName));
            ApplyFilter();
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

public class ProductItem : INotifyPropertyChanged
{
    private Equipment _selectedProduct;
    private int _quantity = 1;

    public Equipment SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            if (_selectedProduct == value) return;
            _selectedProduct = value;
            OnPropertyChanged(nameof(SelectedProduct));
            OnPropertyChanged(nameof(TotalCost)); // Уведомить, что TotalCost изменился
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (_quantity == value) return;
            _quantity = value;
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(TotalCost)); // Уведомить, что TotalCost изменился
        }
    }

    public decimal TotalCost => SelectedProduct?.Cost * Quantity ?? 0;

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}