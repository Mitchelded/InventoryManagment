namespace InventoryManagerMAUI.ViewModels.ViewModel;

// TODO: add viewmodel
public class MaintenanceRecordsViewModel : ViewModelBase<Maintenance>
{
    private decimal? _cost;
    private int _equipmentId;
    private int _idMaintenance;
    private string _maintenanceType;
    private DateTime _maintensnceDate;
    private int? _performedByEmployeeId;

    public MaintenanceRecordsViewModel() : base()
    {
    }

    public int IdMaintenance
    {
        get => _idMaintenance;
        set
        {
            if (value == _idMaintenance) return;
            _idMaintenance = value;
            OnPropertyChanged(nameof(IdMaintenance));
        }
    }


    public int EquipmentId
    {
        get => _equipmentId;
        set
        {
            if (value == _equipmentId) return;
            _equipmentId = value;
            OnPropertyChanged(nameof(EquipmentId));
        }
    }

    public DateTime MaintensnceDate
    {
        get => _maintensnceDate;
        set
        {
            if (value.Equals(_maintensnceDate)) return;
            _maintensnceDate = value;
            OnPropertyChanged(nameof(MaintensnceDate));
        }
    }

    public int? PerformedByEmployeeId
    {
        get => _performedByEmployeeId;
        set
        {
            if (value == _performedByEmployeeId) return;
            _performedByEmployeeId = value;
            OnPropertyChanged(nameof(PerformedByEmployeeId));
        }
    }

    public string MaintenanceType
    {
        get => _maintenanceType;
        set
        {
            if (value == _maintenanceType) return;
            _maintenanceType = value;
            OnPropertyChanged(nameof(MaintenanceType));
        }
    }

    public decimal? Cost
    {
        get => _cost;
        set
        {
            if (value == _cost) return;
            _cost = value;
            OnPropertyChanged(nameof(Cost));
        }
    }
//TODO:Изменить метод
    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities db = new();
        var Category = new Maintenance()
        {
            MaintenanceID = _idMaintenance,
            EquipmentID = _equipmentId,
            // MaintensnceDate = _maintensnceDate,
            // PerformedByEmployeeID = _performedByEmployeeId,
            // MaintenanceType = _maintenanceType,
            // Cost = _cost,
        };
        Collection.Add(Category);
        db.Add(Category);
        db.SaveChanges();
    }
}