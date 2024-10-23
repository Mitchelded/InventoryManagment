using InventoryManagement.Models;
using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class InventoryMovementsViewModel : ViewModelBase<InventoryMovements>
{
    private int _idMovement;
    private int _equipmentId;
    private int _fromLocationId;
    private int _toLocationId;
    private DateTime _movementDate;
    private int _movedByEmployeeId;
    private int? _receivedByEmployeeId;

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


    public InventoryMovementsViewModel() : base()
    {
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();
        var categories = new InventoryMovements()
        {
            IdMovement =_idMovement,
            EquipmentID = _equipmentId,
            FromLocationID = _fromLocationId,
            ToLocationID =_toLocationId,
            MovementDate =_movementDate,
            MovedByEmployeeID =_movedByEmployeeId,
            ReceivedByEmployeeID =_receivedByEmployeeId,
        };
        Collection.Add(categories);
        _db.Add(categories);
        _db.SaveChanges();
    }
}