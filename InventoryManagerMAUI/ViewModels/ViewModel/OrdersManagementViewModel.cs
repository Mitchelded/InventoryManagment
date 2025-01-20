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

    public override async void OnUpdate(OrderDetail obj)
    {
        await using InventoryManagmentEntities _db = new();
        try
        {
            // Assume `obj` contains the OrderID to identify the order to update
            int orderId = obj.OrderID;

            // Retrieve the existing order
            var existingOrder = _db.Orders.Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderID == orderId);

            if (existingOrder == null)
            {
                throw new Exception("Order not found.");
            }

            // Update order fields
            existingOrder.ShippingAddress = ShippingAddress;
            existingOrder.CustomerName = CustomerName;
            existingOrder.Notes = Notes;
            existingOrder.CustomerEmail = CustomerEmail;
            // Update the order date to the current time (if needed)
            existingOrder.OrderDate = DateTime.Now;
            int userID =  int.Parse( Preferences.Get("CurrentUsername", "0"));
            existingOrder.UserID = userID;

            // Update or remove existing order details
            foreach (var existingDetail in existingOrder.OrderDetails.ToList())
            {
                var updatedProduct = Products.FirstOrDefault(p =>
                    p.SelectedProduct.EquipmentID == existingDetail.EquipmentID);

                if (updatedProduct == null || updatedProduct.Quantity <= 0)
                {
                    // Remove details that are no longer in the updated list
                    _db.OrderDetails.Remove(existingDetail);
                }
                else
                {
                    // Update the quantity of existing details
                    existingDetail.Quantity = updatedProduct.Quantity;
                    Products.Remove(updatedProduct); // Remove from the list to handle remaining additions
                }
            }

            // Add new order details for products not already in the order
            foreach (var newProduct in ExistingProducts)
            {
                if (newProduct.Quantity > 0)
                {
                    var newDetail = new OrderDetail()
                    {
                        EquipmentID = newProduct.SelectedProduct.EquipmentID,
                        Quantity = newProduct.Quantity,
                        OrderID = existingOrder.OrderID,
                        Notes = "q"
                    };

                    _db.OrderDetails.Add(newDetail);
                    Collection.Add(newDetail); // Add to the collection if used elsewhere
                }
            }

            _db.SaveChanges(); // Save all changes
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the order and its details.", ex);
        }
    }

    public ObservableCollection<ProductItem> Products { get; set; }

    public ObservableCollection<ProductItem> ExistingProducts
    {
        get => _existingProducts;
        set
        {
            if (Equals(value, _existingProducts)) return;
            _existingProducts = value;
            OnPropertyChanged(nameof(ExistingProducts));
        }
    }

    public void LoadProduct()
    {
        using InventoryManagmentEntities _db = new();
        try
        {
            // Load data from the database and populate the ObservableCollection
            ExistingProducts.Clear();
            var items = _db.OrderDetails
                .Include(e => e.Equipment)
                .Include(e => e.Order)
                .Where(x => x.OrderID == SelectedItem.OrderID)
                .ToList();
            foreach (var item in items)
            {
                ProductItem product = new()
                {
                    Quantity = item.Quantity,
                    SelectedProduct = item.Equipment
                };
                ExistingProducts.Add(product);
            }
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

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
        try
        { 
            int userID =  int.Parse( Preferences.Get("CurrentUsername", "0"));
            List<Order> orders = new List<Order>();

            var orderToAdd = new Order()
            {
                OrderDate = DateTime.Now,
                ShippingAddress = ShippingAddress,
                CustomerName = CustomerName,
                CustomerEmail = CustomerEmail,
                
                UserID = userID,
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
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
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
    private ObservableCollection<ProductItem> _existingProducts = new();

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
        try
        {
            // Load data from the database and populate the ObservableCollection
            var items = _db.Equipments.ToList();
            foreach (var item in items)
            {
                Equipment.Add(item);
            }
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    private void ApplyFilter()
    {
        using InventoryManagmentEntities db = new();
        try
        {
            List<OrderDetail> filtered;

            filtered = db.OrderDetails
                .Include(e => e.Equipment)
                .Include(e => e.Order)
                .ThenInclude(e => e.User)
                .Where(e =>
                    (e.Order.OrderDate >= StartDate) &&
                    (e.Order.OrderDate <= EndDate)
                    && (string.IsNullOrEmpty(FilterName) || e.Order.User.FullName.Contains(FilterName)))
                .ToList();

            Collection.Clear();
            foreach (var item in filtered)
                Collection.Add(item);
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
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

        try
        {
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
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }


    public async override Task LoadData()
    {
        using InventoryManagmentEntities db = new();
        try
        {
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

            // TODO: роверить работоспособность, может крашить
            db.UpdateOrderTotalCosts();
            OnPropertyChanged(nameof(Collection));
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
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