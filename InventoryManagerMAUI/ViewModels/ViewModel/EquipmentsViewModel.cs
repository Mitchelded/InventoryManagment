using System.Collections.ObjectModel;
using System.Windows.Input;
using InventoryManagment.Models;
using System.IO;
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


	private Departments _selectedDepartment;
	private EquipmentStatus _selectedStatus;	
	private Locations _selectedLocation;
	private Suppliers _selectedSupplier;
	private Categories _selectedCategory;

	private ObservableCollection<Departments> _departments = new ObservableCollection<Departments>();
	private ObservableCollection<Categories> _categories = new ObservableCollection<Categories>();
	private ObservableCollection<EquipmentStatus> _equipmentStatus = new ObservableCollection<EquipmentStatus>();
	private ObservableCollection<Locations> _locations = new ObservableCollection<Locations>();
	private ObservableCollection<Suppliers> _suppliers = new ObservableCollection<Suppliers>();

	public int? SelectedDepartmentId => SelectedDepartment?.IdDepartments;
	public int? SelectedSupplierId => SelectedSupplier?.IdSuppliers;	
	public int? SelectedLocationId => SelectedLocation?.IdLocations;
	public int? SelectedStatusId => SelectedStatus?.IdStatus;
	public int? SelectedCategoryId => SelectedCategory?.IdCategories;
	
	public Departments SelectedDepartment
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
	

	public Suppliers SelectedSupplier
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

	public Locations SelectedLocation
	{
		get => _selectedLocation;
		set
		{
			if (_selectedLocation != value)
			{
				_selectedLocation = value;
				OnPropertyChanged(nameof(SelectedLocation));
				OnPropertyChanged(nameof(SelectedLocationId));
				OnPropertyChanged(nameof(SelectedLocation));
				OnPropertyChanged(nameof(SelectedLocationId));
			}
		}
	}

	public EquipmentStatus SelectedStatus
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
	

	public Categories SelectedCategory
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

	public ObservableCollection<Departments> Departments
	{
		get => _departments;
		set
		{
			if (Equals(value, _departments)) return;
			_departments = value;
			OnPropertyChanged(nameof(Departments));
		}
	}

	public ObservableCollection<Categories> Categories
	{
		get => _categories;
		set
		{
			if (Equals(value, _categories)) return;
			_categories = value;
			OnPropertyChanged(nameof(Categories));
		}
	}

	public ObservableCollection<EquipmentStatus> EquipmentStatus
	{
		get => _equipmentStatus;
		set
		{
			if (Equals(value, _equipmentStatus)) return;
			_equipmentStatus = value;
			OnPropertyChanged(nameof(EquipmentStatus));
		}
	}

	public ObservableCollection<Locations> Locations
	{
		get => _locations;
		set
		{
			if (Equals(value, _locations)) return;
			_locations = value;
			OnPropertyChanged(nameof(Locations));
		}
	}

	public ObservableCollection<Suppliers> Suppliers
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
		LoadImageCommand = new Command<Equipments>(LoadImage);
		LoadDepartments();
		LoadCategories();
		LoadLocations();
		LoadSuppliers();
		LoadEquipmentStatus();
		LoadImagesAsync();
	}
		
	private void ApplyFilter()
	{
		List<Equipments> filtered;
		if (Collection.Count != 0)
		{
			filtered = Collection
				.Where(i => SelectedCategory == null || i.Categories.IdCategories == SelectedCategory.IdCategories)
				.Where(i => SelectedStatus == null || i.EquipmentStatus.IdStatus == SelectedStatus.IdStatus)
				.ToList();

		}
		else
		{
			using InventoryManagmentEntities db = new();
			filtered = db.Equipments
				.Include("Categories") // Пример: загрузить связанные данные
				.Include("EquipmentStatus").ToList();
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
	private void LoadImage(Equipments item)
	{
		using InventoryManagmentEntities _db = new();
		// Fetch the user by Id (or any condition you need)
		int idEquipment = item.IdEquipment; // For example, fetch the photo of user with Id = 1

		var equip = _db.Equipments.FirstOrDefault(u => u.IdEquipment == idEquipment);
        
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
	private void LoadCategories()
	{
		using InventoryManagmentEntities _db = new();
		// Load data from the database and populate the ObservableCollection
		var items = _db.Categories.ToList();
		foreach (var item in items)
		{
			Categories.Add(item);
		}
	}
	private void LoadEquipmentStatus()
	{
		using InventoryManagmentEntities _db = new();
		// Load data from the database and populate the ObservableCollection
		var items = _db.EquipmentStatus.ToList();
		foreach (var item in items)
		{
			EquipmentStatus.Add(item);
		}
	}
	private void LoadLocations()
	{
		using InventoryManagmentEntities _db = new();
		// Load data from the database and populate the ObservableCollection
		var items = _db.Locations.ToList();
		foreach (var item in items)
		{
			Locations.Add(item);
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

		var categories = new Equipments()
		{
			Photo = photoBytes,
			Name = _name,
			Serial_Number = _serialNumber,
			CategoryID = (int)SelectedCategoryId!,
			DepartmentID = (int)SelectedDepartmentId!,
			LocationID = (int)SelectedDepartmentId,
			Cost = _cost,
			SupplierID = (int)SelectedDepartmentId,
			WarrantyExpiration = _warrantyExpiration,
			PurchaseDate = _purchaseDate,
			StatusID = (int)SelectedStatusId!,
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