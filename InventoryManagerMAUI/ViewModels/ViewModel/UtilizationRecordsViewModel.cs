using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

// TODO: add viewmodel
public class UtilizationRecordsViewModel : ViewModelBase<UtilizationRecords>
{
    private int? _employeeId;
    private int _equipmentId;
    private int _idUtilization;
    private string _purpose;
    private int? _studentId;
    private DateTime? _usageEnd;
    private DateTime _usageStart;

    public UtilizationRecordsViewModel()
    {
    }

    public int IdUtilization
    {
        get => _idUtilization;
        set
        {
            if (value == _idUtilization) return;
            _idUtilization = value;
            OnPropertyChanged(nameof(IdUtilization));
        }
    }

    public int EquipmentID
    {
        get => _equipmentId;
        set
        {
            if (value == _equipmentId) return;
            _equipmentId = value;
            OnPropertyChanged(nameof(EquipmentID));
        }
    }

    public Nullable<int> StudentID
    {
        get => _studentId;
        set
        {
            if (value == _studentId) return;
            _studentId = value;
            OnPropertyChanged(nameof(StudentID));
        }
    }

    public Nullable<int> EmployeeID
    {
        get => _employeeId;
        set
        {
            if (value == _employeeId) return;
            _employeeId = value;
            OnPropertyChanged(nameof(EmployeeID));
        }
    }

    public DateTime UsageStart
    {
        get => _usageStart;
        set
        {
            if (value.Equals(_usageStart)) return;
            _usageStart = value;
            OnPropertyChanged(nameof(UsageStart));
        }
    }

    public Nullable<DateTime> UsageEnd
    {
        get => _usageEnd;
        set
        {
            if (Nullable.Equals(value, _usageEnd)) return;
            _usageEnd = value;
            OnPropertyChanged(nameof(UsageEnd));
        }
    }

    public string Purpose
    {
        get => _purpose;
        set
        {
            if (value == _purpose) return;
            _purpose = value;
            OnPropertyChanged(nameof(Purpose));
        }
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();
        var status = new UtilizationRecords()
        {
            IdUtilization = _idUtilization,
            EquipmentID = _equipmentId,
            StudentID = _studentId,
            EmployeeID = _employeeId,
            UsageStart = _usageStart,
            UsageEnd = _usageEnd,
            Purpose = _purpose,
        };
        Collection.Add(status);
        _db.Add(status);
        _db.SaveChanges();
    }
}