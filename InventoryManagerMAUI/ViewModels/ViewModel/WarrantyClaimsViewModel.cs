using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class WarrantyClaimsViewModel : ViewModelBase<WarrantyClaim>
{
    public WarrantyClaimsViewModel()
    {
        LoadEquipment();
        LoadStatus();
    }

//TODO: Проверить работоспособность
    public override async void OnUpdate(WarrantyClaim item)
    {
        if (item != null)
        {
            Console.WriteLine($"Updating item: {item}");
            try
            {
                using InventoryManagmentEntities _db = new();

                if (SelectedEquipment != null) item.EquipmentID = SelectedEquipment.EquipmentID;
                item.ClaimDate = ClaimDate;
                item.IssueDescription = _issueDescription;
                item.Resolution = _resolution;
                item.ResolutionDate = ResolutionDate;

                _db.Entry(item).State = EntityState.Modified;
                await _db.SaveChangesAsync();

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

    private void ApplyFilter()
    {
        using InventoryManagmentEntities db = new();
        try
        {
            List<WarrantyClaim> filtered = db.WarrantyClaims
                .Include(e => e.Equipment)
                .Where(e =>
                    (e.ClaimDate >= StartDate)
                    && (e.ClaimDate <= EndDate)
                    && (string.IsNullOrEmpty(FilterName) || e.Equipment.Name.Contains(FilterName))
                    && (SelectedFilterStatus == null || e.StatusID == SelectedFilterStatus.StatusID))
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

    public override async Task LoadData()
    {
        using InventoryManagmentEntities _db = new();
        try
        {
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
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();
        try
        {
            if (SelectedStatus != null)
            {
                if (SelectedEquipment != null)
                {
                    WarrantyClaim warrantyClaim = new()
                    {
                        EquipmentID = SelectedEquipment.EquipmentID,
                        ClaimDate = ClaimDate,
                        IssueDescription = IssueDescription,
                        Resolution = Resolution,
                        StatusID = SelectedStatus.StatusID,
                        ResolutionDate = ResolutionDate
                    };

                    // Add the order detail to the database
                    Collection.Add(warrantyClaim);
                    _db.WarrantyClaims.Add(warrantyClaim);
                }
            }

            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    private void LoadStatus()
    {
        using InventoryManagmentEntities _db = new();
        try
        {
            // Load data from the database and populate the ObservableCollection
            var items = _db.Statuses.ToList();
            foreach (var item in items)
            {
                Status.Add(item);
            }
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
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

    private Status? _selectedStatus;

    public DateTime _claimDate;
    public string _issueDescription;

    public DateTime ClaimDate
    {
        get => _claimDate;
        set
        {
            if (value.Equals(_claimDate)) return;
            _claimDate = value;
            OnPropertyChanged(nameof(ClaimDate));
        }
    }

    public string IssueDescription
    {
        get => _issueDescription;
        set
        {
            if (value == _issueDescription) return;
            _issueDescription = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(IssueDescription));
        }
    }

    public string Resolution
    {
        get => _resolution;
        set
        {
            if (value == _resolution) return;
            _resolution = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(Resolution));
        }
    }

    public DateTime? ResolutionDate
    {
        get => _resolutionDate;
        set
        {
            if (Nullable.Equals(value, _resolutionDate)) return;
            _resolutionDate = value;
            OnPropertyChanged(nameof(ResolutionDate));
        }
    }

    public string _resolution;
    public DateTime? _resolutionDate;

    public Status? SelectedStatus
    {
        get => _selectedStatus;
        set
        {
            if (Equals(value, _selectedStatus)) return;
            _selectedStatus = value;
            OnPropertyChanged(nameof(SelectedStatus));
        }
    }

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

    private Equipment? _selectedEquipment;
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