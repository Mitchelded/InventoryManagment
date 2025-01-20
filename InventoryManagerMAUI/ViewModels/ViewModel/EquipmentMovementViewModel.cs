using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public sealed class EquipmentMovementViewModel : ViewModelBase<EquipmentMovement>
{
    public override async void OnUpdate(EquipmentMovement item)
    {
        if (item != null)
        {
            Console.WriteLine($"Updating item: {item}");
            try
            {
                using InventoryManagmentEntities _db = new();

                item.EquipmentID = SelectedEquipment.EquipmentID;
                item.SourceWarehouseID = SelectedFromLocation.WarehouseID;
                item.DestinationWarehouseID = SelectedToLocation.WarehouseID;
                item.UserID = 1;
                item.MovementDate = MovementDate;
                item.MovementType = MovementType;
                item.Notes = Notes;

                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                await Application.Current.MainPage.DisplayAlert("Update", "Item updated successfully", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error updating item: {ex.Message}", "OK");
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Item is null, reloading data.", "OK");
            LoadData();
        }
    }

    public override async Task LoadData()
    {
        using InventoryManagmentEntities _db = new();
        try
        {
            Collection.Clear();
            await _db.EquipmentMovements
                .Include(e => e.Equipment)
                .ThenInclude(e => e.Status)
                .Include(e => e.Equipment)
                .ThenInclude(e => e.Category)
                .Include(e => e.SourceWarehouse)
                .Include(e => e.DestinationWarehouse)
                .Include(e => e.User)
                .LoadAsync();
            foreach (var item in _db.EquipmentMovements.Local)
            {
                Collection.Add(item);
            }

            OnPropertyChanged(nameof(Collection));
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }


    public EquipmentMovementViewModel()
    {
        LoadData();
        LoadEquipment();
        LoadLocation();
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();
        try
        {
            // Получаем UserID асинхронно
            int? userID =  int.Parse( Preferences.Get("CurrentUsername", "0"));

            EquipmentMovement movement = new()
            {
                EquipmentID = SelectedEquipment.EquipmentID,
                SourceWarehouseID = SelectedFromLocation.WarehouseID,
                DestinationWarehouseID = SelectedToLocation.WarehouseID,
                // Добавляем текущего пользователя, кто создал заказ
                UserID = userID,
                MovementDate = DateTime.Now,
                MovementType = MovementType,
                Notes = Notes
            };

            // Добавляем движение оборудования в коллекцию
            Collection.Add(movement);

            // Добавляем запись в базу данных асинхронно
            _db.EquipmentMovements.Add(movement);

            // Используем асинхронный метод для сохранения изменений
            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            // Отображаем всплывающее окно с ошибкой
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");

            // Логируем ошибку для дальнейшего анализа (опционально)
            Console.WriteLine($"Ошибка в методе OnAdd: {ex.Message}");
        }
    }

    private string _movementType;

    public string MovementType
    {
        get => _movementType;
        set
        {
            if (value == _movementType) return;
            _movementType = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(MovementType));
        }
    }

    private Equipment? _selectedEquipment;
    private Warehouse _selectedToLocation;
    private Warehouse _selectedFromLocation;
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

    private void LoadLocation()
    {
        using InventoryManagmentEntities _db = new();
        try
        {
            // Load data from the database and populate the ObservableCollection
            var items = _db.Warehouses.ToList();
            foreach (var item in items)
            {
                Location.Add(item);
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
            List<EquipmentMovement> filtered;

            filtered = db.EquipmentMovements
                .Include(e => e.Equipment)
                .Include(e => e.SourceWarehouse)
                .Include(e => e.DestinationWarehouse)
                .Where(e =>
                    (string.IsNullOrEmpty(FilterName) || e.Equipment.Name.Contains(FilterName)))
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

    private string _filterName;
    private DateTime _movementDate;
    private string _reasonForMovement;

    public string ReasonForMovement
    {
        get => _reasonForMovement;
        set
        {
            if (value == _reasonForMovement) return;
            _reasonForMovement = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(ReasonForMovement));
        }
    }

    public Warehouse SelectedFromLocation
    {
        get => _selectedFromLocation;
        set
        {
            if (Equals(value, _selectedFromLocation)) return;
            _selectedFromLocation = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(SelectedFromLocation));
        }
    }

    public DateTime MovementDate
    {
        get => _movementDate;
        set
        {
            if (value.Equals(_movementDate)) return;
            _movementDate = value;
            OnPropertyChanged(nameof(MovementDate));
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

    private ObservableCollection<Equipment> _equipment = new();
    private ObservableCollection<Warehouse> _location = new();

    public Equipment? SelectedEquipment
    {
        get => _selectedEquipment;
        set
        {
            if (Equals(value, _selectedEquipment)) return;
            _selectedEquipment = value;
            OnPropertyChanged(nameof(SelectedEquipment));
        }
    }

    public Warehouse SelectedToLocation
    {
        get => _selectedToLocation;
        set
        {
            if (Equals(value, _selectedToLocation)) return;
            _selectedToLocation = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(SelectedToLocation));
        }
    }


    public ObservableCollection<Warehouse> Location
    {
        get => _location;
        set
        {
            if (Equals(value, _location)) return;
            _location = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(Location));
        }
    }

    public ObservableCollection<Equipment> Equipment
    {
        get => _equipment;
        set
        {
            if (Equals(value, _equipment)) return;
            _equipment = value;
            OnPropertyChanged(nameof(Equipment));
        }
    }
}