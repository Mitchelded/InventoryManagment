namespace InventoryManagerMAUI.ViewModels.ViewModel;

// TODO: add viewmodel
public class SuppliersViewModel : ViewModelBase<Supplier>
{
    private string _adress;
    private string _contactInfo;
    private string _email;
    private int _idSuppliers;
    private string _name;

    public SuppliersViewModel()
    {
    }

    public int IdSuppliers
    {
        get => _idSuppliers;
        set
        {
            if (value == _idSuppliers) return;
            _idSuppliers = value;
            OnPropertyChanged(nameof(IdSuppliers));
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string ContactInfo
    {
        get => _contactInfo;
        set
        {
            if (value == _contactInfo) return;
            _contactInfo = value;
            OnPropertyChanged(nameof(ContactInfo));
        }
    }

    public string Adress
    {
        get => _adress;
        set
        {
            if (value == _adress) return;
            _adress = value;
            OnPropertyChanged(nameof(Adress));
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (value == _email) return;
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
//TODO:Изменить метод
    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();
        var status = new Supplier()
        {
            SupplierID = _idSuppliers,
            Name = _name,
            ContactInfo = _contactInfo,
            // Adress = _adress,
            // Email = _email,
        };
        Collection.Add(status);
        _db.Add(status);
        _db.SaveChanges();
    }
}