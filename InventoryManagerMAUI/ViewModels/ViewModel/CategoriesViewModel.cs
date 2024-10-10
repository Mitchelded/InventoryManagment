using System.Collections.ObjectModel;
using System.Windows.Input;
using InventoryManagerMAUI.Interface;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class CategoriesViewModel : ViewModelBase<Categories>
{

    public CategoriesViewModel() : base()
	{

    }
    private int _idCategories;
    private string _name;
    private string _description;


    public int IdCategories
    {
        get => _idCategories;
        set
        {
            if (value == _idCategories) return;
            _idCategories = value;
            OnPropertyChanged(nameof(IdCategories));
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

    public override void OnAdd(object obj)
    {
        throw new NotImplementedException();
    }
}