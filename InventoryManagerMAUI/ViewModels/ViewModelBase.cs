using System.ComponentModel;
using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{
    protected InventoryManagmentEntities _db;

    public ViewModelBase()
    {
        _db = new();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}