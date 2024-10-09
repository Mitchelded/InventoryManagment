using System.Collections.ObjectModel;
using System.Windows.Input;
using InventoryManagerMAUI.Interface;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels;

public class EquipmentStatusViewModel : ViewModelBase, IDbCommands<EquipmentStatus>
{


	public ObservableCollection<EquipmentStatus> Collection { get; set; }

	private EquipmentStatus _selectedStatus;

	public EquipmentStatus SelectedStatus
	{
		get => _selectedStatus;
		set 
			{ _selectedStatus = value; OnPropertyChanged(nameof(SelectedStatus));}

	}

	public ICommand DeleteCommand { get; }
	public ICommand UpdateCommand { get; }
	public ICommand AddCommand { get; }

	// private readonly InventoryManagmentEntities _db;
	public EquipmentStatusViewModel()
	{
		// _db = new();
		LoadData();
		DeleteCommand = new Command<EquipmentStatus>(OnDelete);
		UpdateCommand = new Command<EquipmentStatus>(OnUpdate);
		AddCommand = new Command(OnAdd);
	}

	public void OnAdd(object obj)
	{
		var status = new EquipmentStatus()
		{
			Name = _name,
			Description = _description
		};
		Collection.Add(status);
		_db.SaveChanges();
	}

	public void LoadData()
	{
		_db.EquipmentStatus.Load();
		Collection = null;
		Collection = _db.EquipmentStatus.Local.ToObservableCollection();
	}

	public void OnUpdate(EquipmentStatus status)
	{
		if (status != null)
		{
			var q = _db.EquipmentStatus.Find(status.IdStatus);
			status = q;
			_db.SaveChanges();
		}
		else
		{
			LoadData();
		}
	}

	public void OnDelete(EquipmentStatus status)
	{
		if (status != null)
		{
			//EquipmentStatusMethods methods = new();
			//methods.Remove(status);
			Collection.Remove(status);

			_db.SaveChanges();
		}
	}

	private int _idStatus;
	private string _description;
	private string _name;
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