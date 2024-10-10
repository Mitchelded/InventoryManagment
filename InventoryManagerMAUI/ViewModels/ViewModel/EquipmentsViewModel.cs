using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class EquipmentsViewModel : ViewModelBase<Equipments>
{
	private int _idEquipment;
	private string _name;
	private string _serialNumber;
	private int _categoryId;
	private int _departmentId;
	private int _locationId;
	private decimal? _cost;
	private int? _supplierId;
	private DateTime? _warrantyExpiration;
	private DateTime _purchaseDate;
	private int _statusId;
	public EquipmentsViewModel() : base()
	{

	}

	public override void OnAdd(object obj)
	{
		using InventoryManagmentEntities _db = new();
		var categories = new Equipments()
		{
			Name = _name,
			Serial_Number = _serialNumber,
			CategoryID = _categoryId,
			DepartmentID = _departmentId,
			LocationID = _locationId,
			Cost = _cost,
			SupplierID = _supplierId,
			WarrantyExpiration = _warrantyExpiration,
			PurchaseDate = _purchaseDate,
			StatusID = _statusId,
		};
		Collection.Add(categories);
		_db.Add(categories);
		_db.SaveChanges();
	}

	public int IdEquipment
	{
		get => _idEquipment;
		set
		{
			if (value == _idEquipment) return;
			_idEquipment = value;
			OnPropertyChanged(nameof(IdEquipment));
			OnPropertyChanged(nameof(IdEquipment));
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

	public string Serial_Number
	{
		get => _serialNumber;
		set
		{
			if (value == _serialNumber) return;
			_serialNumber = value;
			OnPropertyChanged(nameof(Serial_Number));
			OnPropertyChanged(nameof(Serial_Number));
		}
	}

	public int CategoryID
	{
		get => _categoryId;
		set
		{
			if (value == _categoryId) return;
			_categoryId = value;
			OnPropertyChanged(nameof(CategoryID));
			OnPropertyChanged(nameof(CategoryID));
		}
	}

	public int DepartmentID
	{
		get => _departmentId;
		set
		{
			if (value == _departmentId) return;
			_departmentId = value;
			OnPropertyChanged(nameof(DepartmentID));
			OnPropertyChanged(nameof(DepartmentID));
		}
	}

	public int LocationID
	{
		get => _locationId;
		set
		{
			if (value == _locationId) return;
			_locationId = value;
			OnPropertyChanged(nameof(LocationID));
		}
	}

	public int StatusID
	{
		get => _statusId;
		set
		{
			if (value == _statusId) return;
			_statusId = value;
			OnPropertyChanged(nameof(StatusID));
		}
	}

	public System.DateTime PurchaseDate
	{
		get => _purchaseDate;
		set
		{
			if (value.Equals(_purchaseDate)) return;
			_purchaseDate = value;
			OnPropertyChanged(nameof(PurchaseDate));
		}
	}

	public System.DateTime? WarrantyExpiration
	{
		get => _warrantyExpiration;
		set
		{
			if (Nullable.Equals(value, _warrantyExpiration)) return;
			_warrantyExpiration = value;
			OnPropertyChanged(nameof(WarrantyExpiration));
		}
	}

	public Nullable<int> SupplierID
	{
		get => _supplierId;
		set
		{
			if (value == _supplierId) return;
			_supplierId = value;
			OnPropertyChanged(nameof(SupplierID));
		}
	}

	public Nullable<decimal> Cost
	{
		get => _cost;
		set
		{
			if (value == _cost) return;
			_cost = value;
			OnPropertyChanged(nameof(Cost));
		}
	}
}