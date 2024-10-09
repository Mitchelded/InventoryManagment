using System.Collections.ObjectModel;
using System.Windows.Input;
using InventoryManagerMAUI.Interface;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels;

public class CategoriesViewModel : ViewModelBase ,IDbCommands<Categories>
{
    private readonly InventoryManagmentEntities _db;

    public CategoriesViewModel()
    {
        _db = new();
        LoadData();
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


    public ObservableCollection<Categories> Collection { get; set; }
    public ICommand DeleteCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand AddCommand { get; }

    public void OnAdd(object obj)
    {
        throw new NotImplementedException();
    }

    public void LoadData()
    {
        _db.Categories.Load();
        Collection = _db.Categories.Local.ToObservableCollection();
    }

    public void OnUpdate(Categories status)
    {
        throw new NotImplementedException();
    }

    public void OnDelete(Categories status)
    {
        throw new NotImplementedException();
    }
}