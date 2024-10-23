using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class MaintenanceRecordsViewModel : ViewModelBase<MaintenanceRecords>
{
    private int _idMaintenance;
    private int _equipmentId;
    private DateTime _maintensnceDate;
    private int? _performedByEmployeeId;
    private string _maintenanceType;
    private decimal? _cost;

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

    public MaintenanceRecordsViewModel() : base()
    {
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities db = new();
        var categories = new MaintenanceRecords()
        {
            IdMaintenance = _idMaintenance,
            EquipmentID = _equipmentId,
            MaintensnceDate = _maintensnceDate,
            PerformedByEmployeeID = _performedByEmployeeId,
            MaintenanceType = _maintenanceType,
            Cost = _cost,
        };
        Collection.Add(categories);
        db.Add(categories);
        db.SaveChanges();
    }
}