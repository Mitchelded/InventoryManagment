using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

// TODO: add viewmodel
public class WarrantyClaimsViewModel : ViewModelBase<WarrantyClaims>
{
    private DateTime _claimDate;
    private int _equipmentId;
    private int _idWarranty;
    private string _issueDescription;
    private DateTime? _resolvedDate;
    private string _status;

    public int IdWarranty
    {
        get => _idWarranty;
        set
        {
            if (value == _idWarranty) return;
            _idWarranty = value;
            OnPropertyChanged(nameof(IdWarranty));
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
            _issueDescription = value;
            OnPropertyChanged(nameof(IssueDescription));
        }
    }

    public string Status
    {
        get => _status;
        set
        {
            if (value == _status) return;
            _status = value;
            OnPropertyChanged(nameof(Status));
        }
    }

    public DateTime? ResolvedDate
    {
        get => _resolvedDate;
        set
        {
            if (Nullable.Equals(value, _resolvedDate)) return;
            _resolvedDate = value;
            OnPropertyChanged(nameof(ResolvedDate));
        }
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();
        var status = new WarrantyClaims
        {
            IdWarranty = _idWarranty,
            EquipmentID = _equipmentId,
            ClaimDate = _claimDate,
            IssueDescription = _issueDescription,
            Status = _status,
            ResolvedDate = _resolvedDate
        };
        Collection.Add(status);
        _db.Add(status);
        _db.SaveChanges();
    }
}