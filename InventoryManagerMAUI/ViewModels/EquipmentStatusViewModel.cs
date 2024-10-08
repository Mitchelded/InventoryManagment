using System.Collections.ObjectModel;
using System.Windows.Input;
using InventoryManagerMAUI.Commands.DbMethods;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels;

public class EquipmentStatusViewModel : ViewModelBase
{
	public ObservableCollection<EquipmentStatus> EquipmentStatus { get; set; }

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

	private readonly InventoryManagmentEntities db;
	public EquipmentStatusViewModel()
	{
		db = new();
		db.Database.EnsureCreated();
		LoadData();
		DeleteCommand = new Command<EquipmentStatus>(OnDelete);
		UpdateCommand = new Command<EquipmentStatus>(OnUpdate);
		AddCommand = new Command(OnAdd);
	}

	private void OnAdd(object obj)
	{
		var status = new EquipmentStatus()
		{
			Name = _name,
			Description = _description
		};
		EquipmentStatus.Add(status);
		db.SaveChanges();
	}

	private void LoadData()
	{
		db.EquipmentStatus.Load();
		EquipmentStatus = null;
		EquipmentStatus = db.EquipmentStatus.Local.ToObservableCollection();
	}

	private void OnUpdate(EquipmentStatus status)
	{
		if (status != null)
		{
			var q = db.EquipmentStatus.Find(status.IdStatus);
			status = q;
			db.SaveChanges();
		}
		else
		{
			LoadData();
		}
	}

	private void OnDelete(EquipmentStatus status)
	{
		if (status != null)
		{
			//EquipmentStatusMethods methods = new();
			//methods.Remove(status);
			EquipmentStatus.Remove(status);

			db.SaveChanges();
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