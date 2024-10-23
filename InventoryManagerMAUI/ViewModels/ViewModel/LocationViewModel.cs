using InventoryManagement.Models;
using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class LocationViewModel : ViewModelBase<Locations>
{
    private int _idLocations;
    private string _description;
    private int _departmentId;

    public int IdLocations
    {
        get => _idLocations;
        set
        {
            if (value == _idLocations) return;
            _idLocations = value;
            OnPropertyChanged(nameof(IdLocations));
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (value == _description) return;
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    public int DepartmentID
    {
        get => _departmentId;
        set
        {
            if (value == _departmentId) return;
            _departmentId = value;
            OnPropertyChanged(nameof(DepartmentID));
        }
    }

    public LocationViewModel() : base()
    {
        
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();

        var categories = new Locations()
        {
            IdLocations = _idLocations,
            Description = _description,
            DepartmentID = _departmentId,
        };
        Collection.Add(categories);
        _db.Add(categories);
        _db.SaveChanges();
    }
}