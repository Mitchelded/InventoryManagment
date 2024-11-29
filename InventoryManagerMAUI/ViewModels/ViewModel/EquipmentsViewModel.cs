using System.Collections.ObjectModel;
using System.Windows.Input;
using System.IO;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class EquipmentsViewModel : ViewModelBase<Equipment>
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
	private byte[] _photo;    
	
	private List<ImageSource> _imageSources;

	public List<ImageSource> ImageSources
	{
		get => _imageSources;
		set
		{
			_imageSources = value;
			OnPropertyChanged(nameof(ImageSources));
		}
	}
	private async Task LoadImagesAsync()
	{
		// Получаем все байты изображений для всех объектов оборудования
		var imageByteList = await GetAllEquipmentImagesAsync();
        
		var imageSources = new List<ImageSource>();

		foreach (var imageBytes in imageByteList)
		{
			if (imageBytes != null)
			{
				// Преобразуем байты в ImageSource
				imageSources.Add(ImageSource.FromStream(() => new MemoryStream(imageBytes)));
			}
		}

		// Привязываем к свойству
		ImageSources = imageSources;
	}

	// Метод для получения байтов всех изображений из базы данных
	private async Task<List<byte[]>> GetAllEquipmentImagesAsync()
	{
		using (var context = new InventoryManagmentEntities()) // Замените на свой контекст
		{
			var equipmentImages = await context.Equipments
				.Select(eq => eq.Photo) // Извлекаем все байты из поля Photo
				.ToListAsync();

			return equipmentImages;
		}
	}
	public byte[] Photo
	{
		get => _photo;
		set
		{
			if (Equals(value, _photo)) return;
			_photo = value;
			OnPropertyChanged(nameof(Photo));
		}
	}


	private Department _selectedDepartment;
	private Status _selectedStatus;	

	private Supplier _selectedSupplier;
	private Category _selectedCategory;

	private ObservableCollection<Department> _departments = new ObservableCollection<Department>();
	private ObservableCollection<Category> _Category = new ObservableCollection<Category>();
	private ObservableCollection<Status> _Status = new ObservableCollection<Status>();

	private ObservableCollection<Supplier> _suppliers = new ObservableCollection<Supplier>();

	public int? SelectedDepartmentId => SelectedDepartment?.DepartmentID;
	public int? SelectedSupplierId => SelectedSupplier?.SupplierID;	

	public int? SelectedStatusId => SelectedStatus?.StatusID;
	public int? SelectedCategoryId => SelectedCategory?.CategoryID;
	
	public Department SelectedDepartment
	{
		get => _selectedDepartment;
		set
		{
			if (_selectedDepartment != value)
			{
				_selectedDepartment = value;
				OnPropertyChanged(nameof(SelectedDepartment));
				OnPropertyChanged(nameof(SelectedDepartmentId));
				OnPropertyChanged(nameof(SelectedDepartment));
				OnPropertyChanged(nameof(SelectedDepartmentId));
			}
		}
	}
	

	public Supplier SelectedSupplier
	{
		get => _selectedSupplier;
		set
		{
			if (_selectedSupplier != value)
			{
				_selectedSupplier = value;
				OnPropertyChanged(nameof(SelectedSupplier));
				OnPropertyChanged(nameof(SelectedSupplierId));
				OnPropertyChanged(nameof(SelectedSupplier));
				OnPropertyChanged(nameof(SelectedSupplierId));
			}
		}
	}
	

	public Status SelectedStatus
	{
		get => _selectedStatus;
		set
		{
			if (_selectedStatus != value)
			{
				_selectedStatus = value;
				OnPropertyChanged(nameof(SelectedStatus));
				OnPropertyChanged(nameof(SelectedStatusId));
				OnPropertyChanged(nameof(SelectedStatus));
				OnPropertyChanged(nameof(SelectedStatusId));
				ApplyFilter();
			}
		}
	}
	

	public Category SelectedCategory
	{
		get => _selectedCategory;
		set
		{
			if (_selectedCategory != value)
			{
				_selectedCategory = value;
				OnPropertyChanged(nameof(SelectedCategory));
				OnPropertyChanged(nameof(SelectedCategoryId));
				OnPropertyChanged(nameof(SelectedCategory));
				OnPropertyChanged(nameof(SelectedCategoryId));
				ApplyFilter();
			}
		}
	}

	public ObservableCollection<Department> Departments
	{
		get => _departments;
		set
		{
			if (Equals(value, _departments)) return;
			_departments = value;
			OnPropertyChanged(nameof(Departments));
		}
	}

	public ObservableCollection<Category> Category
	{
		get => _Category;
		set
		{
			if (Equals(value, _Category)) return;
			_Category = value;
			OnPropertyChanged(nameof(Category));
		}
	}

	public ObservableCollection<Status> Status
	{
		get => _Status;
		set
		{
			if (Equals(value, _Status)) return;
			_Status = value;
			OnPropertyChanged(nameof(Status));
		}
	}



	public ObservableCollection<Supplier> Suppliers
	{
		get => _suppliers;
		set
		{
			if (Equals(value, _suppliers)) return;
			_suppliers = value;
			OnPropertyChanged(nameof(Suppliers));
		}
	}
	public ICommand LoadImageCommand { get; }
	
	public EquipmentsViewModel()
	{
		LoadImageCommand = new Command<Equipment>(LoadImage);
		// LoadDepartments();
		// LoadCategory();
		//
		// LoadSuppliers();
		// LoadStatus();
		// LoadImagesAsync();
	}
		
	private void ApplyFilter()
	{
		List<Equipment> filtered;
		if (Collection.Count != 0)
		{
			filtered = Collection
				.Where(i => SelectedCategory == null || i.Category.CategoryID == SelectedCategory.CategoryID)
				.Where(i => SelectedStatus == null || i.Status.StatusID == SelectedStatus.StatusID)
				.ToList();

		}
		else
		{
			using InventoryManagmentEntities db = new();
			filtered = db.Equipments
				.Include("Category") // Пример: загрузить связанные данные
				.Include("Status").ToList();
		}


		Collection.Clear();
		foreach (var item in filtered)
			Collection.Add(item);
	}
	
	
	private ImageSource _equipmentImageSource;

	public ImageSource EquipmentImageSource
	{
		get { return _equipmentImageSource; }
		set { SetProperty(ref _equipmentImageSource, value); }
	}
	private void LoadImage(Equipment item)
	{
		using InventoryManagmentEntities _db = new();
		// Fetch the user by Id (or any condition you need)
		int idEquipment = item.EquipmentID; // For example, fetch the photo of user with Id = 1

		var equip = _db.Equipments.FirstOrDefault(u => u.EquipmentID == idEquipment);
        
		if (equip != null && equip.Photo != null)
		{
			// Convert the byte array to a StreamImageSource
			var imageStream = new MemoryStream(equip.Photo);
			EquipmentImageSource = ImageSource.FromStream(() => imageStream);  // Set the ImageSource
		}
		else
		{
			// Handle case where user is not found or photo is null
			Console.WriteLine("User or photo not found!");
		}
	}
	
	private void LoadDepartments()
	{
		using InventoryManagmentEntities _db = new();
		// Load data from the database and populate the ObservableCollection
		var items = _db.Departments.ToList();
		foreach (var item in items)
		{
			Departments.Add(item);
		}
	}
	private void LoadCategory()
	{
		using InventoryManagmentEntities _db = new();
		// Load data from the database and populate the ObservableCollection
		var items = _db.Category.ToList();
		foreach (var item in items)
		{
			Category.Add(item);
		}
	}
	private void LoadStatus()
	{
		using InventoryManagmentEntities _db = new();
		// Load data from the database and populate the ObservableCollection
		var items = _db.Statuses.ToList();
		foreach (var item in items)
		{
			Status.Add(item);
		}
	}
	private void LoadSuppliers()
	{
		using InventoryManagmentEntities _db = new();
		// Load data from the database and populate the ObservableCollection
		var items = _db.Suppliers.ToList();
		foreach (var item in items)
		{
			Suppliers.Add(item);
		}
	}

	
	public override void OnAdd(object obj)
	{
		using InventoryManagmentEntities _db = new();
		string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/AppIcon", "gag.png");
		byte[] photoBytes = File.ReadAllBytes(filePath);

		var Category = new Equipment()
		{
			Photo = photoBytes,
			Name = _name,
			SerialNumber = _serialNumber,
			CategoryID = (int)SelectedCategoryId!,
			Cost = _cost,
			// SupplierID = (int)SelectedDepartmentId,
			WarrantyExpiration = _warrantyExpiration,
			PurchaseDate = _purchaseDate,
			StatusID = (int)SelectedStatusId!,
		};
		Collection.Add(Category);
		_db.Add(Category);
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
			OnPropertyChanged(nameof(Cost));
		}
	}
}