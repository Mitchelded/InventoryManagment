using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class WarrantyClaimsViewModel : ViewModelBase<WarrantyClaim>
{
    public WarrantyClaimsViewModel()
    {
        LoadEquipment();
        LoadStatus();
    }
    
    private void ApplyFilter()
    {
        using InventoryManagmentEntities db = new();

        List<WarrantyClaim> filtered = db.WarrantyClaims
            .Include(e => e.Equipment)
            .Where(e =>
                (e.ClaimDate >= StartDate)
                && (e.ClaimDate <= EndDate)
                && (string.IsNullOrEmpty(FilterName) || e.Equipment.Name.Contains(FilterName))
                && (SelectedFilterStatus ==null || e.StatusID==SelectedFilterStatus.StatusID))
            .ToList();

        Collection.Clear();
        foreach (var item in filtered)
            Collection.Add(item);
    }
    
    public override async Task LoadData()
    {
        using InventoryManagmentEntities _db = new();
        Collection.Clear();
        await _db.WarrantyClaims
            .Include(e => e.Equipment)
            .ThenInclude(e => e.Status)
            .Include(e => e.Equipment)
            .ThenInclude(e => e.Category)
            .LoadAsync();
        foreach (WarrantyClaim item in _db.WarrantyClaims.Local)
        {
            Collection.Add(item);
        }

        OnPropertyChanged(nameof(Collection));
    }

    public override void OnAdd(object obj)
    {
        base.OnAdd(obj);
    }

    private void LoadStatus()
    {
        using InventoryManagmentEntities _db = new();
        // Load data from the database and populate the ObservableCollection
        var items = _db.Statuses.ToList();
        foreach (var item in items)
        {
            Status.Add(item);
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

    private Status _selectedStatus;

    public Status SelectedStatus
    {
        get => _selectedStatus;
        set
        {
            if (Equals(value, _selectedStatus)) return;
            _selectedStatus = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(SelectedStatus));
        }
    }

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

    public List<Equipment> Equipment
    {
        get => _equipment;
        set
        {
            if (Equals(value, _equipment)) return;
            _equipment = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(Equipment));
        }
    }

    private Equipment _selectedEquipment;
    private List<Equipment> _equipment = new();
    
    
    private List<Status> _status = new();

    public List<Status> Status
    {
        get => _status;
        set
        {
            if (Equals(value, _status)) return;
            _status = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(Status));
        }
    }

    private Status _selectedFilterStatus;

    public Status SelectedFilterStatus
    {
        get => _selectedFilterStatus;
        set
        {
            if (Equals(value, _selectedFilterStatus)) return;
            _selectedFilterStatus = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(SelectedFilterStatus));
        }
    }

    private DateTime _startDate;
    private DateTime _endDate;
    private string _filterName;
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

}