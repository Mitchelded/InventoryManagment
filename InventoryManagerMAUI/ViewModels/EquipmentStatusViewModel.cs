using System.Collections.ObjectModel;
using System.Windows.Input;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels;

public class EquipmentStatusViewModel : ViewModelBase
{
    public ObservableCollection<EquipmentStatus> EquipmentStatus { get; set; }
	public ICommand DeleteCommand { get; }
    private readonly InventoryManagmentEntities db;
	public EquipmentStatusViewModel()
    {
        db = new();
        db.Database.EnsureCreated();
        db.EquipmentStatus.Load();
        EquipmentStatus = db.EquipmentStatus.Local.ToObservableCollection();
        DeleteCommand = new Command<EquipmentStatus>(OnDelete);
	}



	private async void OnDelete(EquipmentStatus status)
	{
        if(status != null) {
			db.EquipmentStatus.Remove(status);
			await db.SaveChangesAsync();
		}


	}

	private int _idStatus;
    private string _description;
    private string _name;
	private EquipmentStatus selectedStatus;

	public int IdStatus
    {
        get => _idStatus;
        set
        {
            if (value == _idStatus) return;
            _idStatus = value;
            OnPropertyChanged(nameof(IdStatus));
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
}