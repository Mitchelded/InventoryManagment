using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

// TODO: add viewmodel
public class InventoryMovementsViewModel : ViewModelBase<EquipmentMovement>
{
    private int _equipmentId;
    private int _fromLocationId;
    private int _idMovement;
    private int _movedByEmployeeId;
    private DateTime _movementDate;
    private int? _receivedByEmployeeId;
    private int _toLocationId;


    public InventoryMovementsViewModel() : base()
    {
    }

    public int IdMovement
    {
        get => _idMovement;
        set
        {
            if (value == _idMovement) return;
            _idMovement = value;
            OnPropertyChanged(nameof(IdMovement));
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

    public int FromLocationID
    {
        get => _fromLocationId;
        set
        {
            if (value == _fromLocationId) return;
            _fromLocationId = value;
            OnPropertyChanged(nameof(FromLocationID));
        }
    }

    public int ToLocationID
    {
        get => _toLocationId;
        set
        {
            if (value == _toLocationId) return;
            _toLocationId = value;
            OnPropertyChanged(nameof(ToLocationID));
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

    public int MovedByEmployeeID
    {
        get => _movedByEmployeeId;
        set
        {
            if (value == _movedByEmployeeId) return;
            _movedByEmployeeId = value;
            OnPropertyChanged(nameof(MovedByEmployeeID));
        }
    }


    public int? ReceivedByEmployeeID
    {
        get => _receivedByEmployeeId;
        set
        {
            if (value == _receivedByEmployeeId) return;
            _receivedByEmployeeId = value;
            OnPropertyChanged(nameof(ReceivedByEmployeeID));
        }
    }
// TODO:Добавить новые данные в add
    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();
        var Category = new EquipmentMovement()
        {
            EquipmentMovementID = _idMovement,
            EquipmentID = _equipmentId,
            SourceWarehouseID = _fromLocationId,
            DestinationWarehouseID = _toLocationId,
            MovementDate = _movementDate,
            UserID = _movedByEmployeeId,
        };
        Collection.Add(Category);
        _db.Add(Category);
        _db.SaveChanges();
    }
}