using System.Collections.ObjectModel;
using System.Windows.Input;

using InventoryManagerMAUI.Interface;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class EquipmentStatusViewModel : ViewModelBase<EquipmentStatus>
{


	//public ObservableCollection<EquipmentStatus> Collection { get; set; }

	private EquipmentStatus _selectedStatus;

	public EquipmentStatus SelectedStatus
	{
		get => _selectedStatus;
		set
		{ _selectedStatus = value; OnPropertyChanged(nameof(SelectedStatus)); }

	}

	public EquipmentStatusViewModel() : base()
	{

	}

	public override void OnAdd(object obj)
	{
		using InventoryManagmentEntities _db = new();
		var status = new EquipmentStatus()
		{
			Name = _name,
			Description = _description
		};
		
		var q =_db.EquipmentStatus.Add(status);
		Collection.Add(status);
		_db.SaveChanges();
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