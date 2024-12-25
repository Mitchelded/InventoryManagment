using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

// TODO: Реализовать 
public sealed class EquipmentMovementViewModel : ViewModelBase<EquipmentMovement>
{
    public override async void LoadData()
    {
        using InventoryManagmentEntities _db = new();
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


    public EquipmentMovementViewModel()
    {
        LoadData();
        LoadEquipment();
        LoadLocation();
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();
        EquipmentMovement movement = new()
        {
            EquipmentID = SelectedEquipment.EquipmentID,
            SourceWarehouseID = SelectedFromLocation.WarehouseID,
            DestinationWarehouseID = SelectedToLocation.WarehouseID,
            // TODO: Добавить добавление текущего пользователя кто создал заказ. После добавления логина
            UserID = 1,
            MovementDate = DateTime.Now,
            MovementType = MovementType,
            Notes = Notes
        };

        // Add the order detail to the database
        Collection.Add(movement);
        _db.EquipmentMovements.Add(movement);
        _db.SaveChanges();
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

    private Equipment _selectedEquipment;
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
        // Load data from the database and populate the ObservableCollection
        var items = _db.Equipments.ToList();
        foreach (var item in items)
        {
            Equipment.Add(item);
        }
    }

    private void LoadLocation()
    {
        using InventoryManagmentEntities _db = new();
        // Load data from the database and populate the ObservableCollection
        var items = _db.Warehouses.ToList();
        foreach (var item in items)
        {
            Location.Add(item);
        }
    }

    private void ApplyFilter()
    {
        using InventoryManagmentEntities db = new();
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

    public Equipment SelectedEquipment
    {
        get => _selectedEquipment;
        set
        {
            if (Equals(value, _selectedEquipment)) return;
            _selectedEquipment = value ?? throw new ArgumentNullException(nameof(value));
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