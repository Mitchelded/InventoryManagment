using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class EquipmentsViewModel : ViewModelBase<Equipment>
{
	private int _idEquipment;
	private string _name;
	private string _serialNumber;
	private string _fileName;
	private int _categoryId;
	private int _departmentId;	
	private int _locationId;
	private decimal? _cost;
	private int? _supplierId;
	private DateTime _warrantyExpiration;
	private DateTime _purchaseDate;
	private int _statusId;
	private byte[] _photo;
	private List<ImageSource> _imageSources;
	private Department _selectedFilterDepartment;

	private Status _selectedStatus;
	private Category _selectedCategory;
	private Supplier _selectedSupplier;

	public Status SelectedStatus
	{
		get => _selectedStatus;
		set
		{
			if (Equals(value, _selectedStatus)) return;
			_selectedStatus = value ?? throw new ArgumentNullException(nameof(value));
			OnPropertyChanged(nameof(SelectedStatus));
		}
	}

	public override async void OnUpdate(Equipment item)
	{
		if (item != null)
		{
			Console.WriteLine($"Updating item: {item}");
			try
			{
				using InventoryManagmentEntities _db = new();

				item.Name = _name;
				item.Photo = _photo;
				item.CategoryID = _selectedCategory?.CategoryID ?? item.CategoryID;
				item.Cost = _cost;
				item.SerialNumber = _serialNumber;
				item.PurchaseDate = _purchaseDate;
				item.WarrantyExpiration = _warrantyExpiration;
				item.StatusID = _selectedStatus?.StatusID ?? item.StatusID;
				item.SupplierID = _selectedSupplier?.SupplierID ?? item.SupplierID;

				_db.Entry(item).State = EntityState.Modified;
				_db.SaveChanges();

				await Application.Current.MainPage.DisplayAlert("Update", "Item updated successfully", "OK");
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", $"Error updating item: {ex.Message}", "OK");
			}
		}
		else
		{
			await Application.Current.MainPage.DisplayAlert("Error", "Item is null, reloading data.", "OK");
			LoadData();
		}
	}


	public Category SelectedCategory
	{
		get => _selectedCategory;
		set
		{
			if (Equals(value, _selectedCategory)) return;
			_selectedCategory = value ?? throw new ArgumentNullException(nameof(value));
			OnPropertyChanged(nameof(SelectedCategory));
		}
	}

	public Supplier SelectedSupplier
	{
		get => _selectedSupplier;
		set
		{
			if (Equals(value, _selectedSupplier)) return;
			_selectedSupplier = value ?? throw new ArgumentNullException(nameof(value));
			OnPropertyChanged(nameof(SelectedSupplier));
		}
	}

	private Status _selectedFilterStatus;	
	private Supplier _selectedFilterSupplier;
	private Category _selectedFilterCategory;
	
	private ImageSource _equipmentImageSource;
	private ObservableCollection<Stock> _stocks;
	
	public int? SelectedDepartmentId => SelectedFilterDepartment?.DepartmentID;
	public int? SelectedSupplierId => SelectedSupplier?.SupplierID;	
	public int? SelectedStatusId => SelectedStatus?.StatusID;
	public int? SelectedCategoryId => SelectedCategory?.CategoryID;

	private ObservableCollection<Department> _departments = new ObservableCollection<Department>();
	private ObservableCollection<Category> _Category = new ObservableCollection<Category>();
	private ObservableCollection<Status> _Status = new ObservableCollection<Status>();
	private ObservableCollection<Supplier> _suppliers = new ObservableCollection<Supplier>();
	public ICommand LoadImageCommand { get; }
	public ICommand SelectImageCommand { get; }
	
	public EquipmentsViewModel()
	{
		SelectImageCommand = new Command(OnSelectImage);
		LoadImageCommand = new Command<Equipment>(LoadImage);
		// LoadDepartments();
		LoadCategory();
		// LoadStocks();
		LoadSuppliers();
		LoadStatus();
		// LoadImagesAsync();
	}
	
	private async void OnSelectImage()
	{
		// Логика выбора изображения и преобразования его в массив байт
		var result = await MediaPicker.PickPhotoAsync();
		if (result != null)
		{
			using var stream = await result.OpenReadAsync();
			using var memoryStream = new MemoryStream();
			await stream.CopyToAsync(memoryStream);
			FileName = result.FileName;
			Photo = memoryStream.ToArray();
		}
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

	private void LoadStocks()
	{
		using InventoryManagmentEntities _db = new();
		// Load data from the database and populate the ObservableCollection
		var items = _db.Stocks.ToList();
		foreach (var item in items)
		{
			Stocks.Add(item);
		}
	}

	public override void OnResete(Equipment obj)
	{
		LoadData();
		SelectedFilterCategory = null;
		SelectedFilterStatus = null;
	}

	private void ApplyFilter()
	{
		using InventoryManagmentEntities db = new();
		List<Equipment> filtered;

		filtered = db.Equipments
			.Include(e => e.Category)
			.Include(e => e.Status)
			.Where(e => 
				(SelectedFilterCategory == null || e.Category.CategoryID == SelectedFilterCategory.CategoryID) &&
				(SelectedFilterStatus == null || e.Status.StatusID == SelectedFilterStatus.StatusID))
			.ToList();




		Collection.Clear();
		foreach (var item in filtered)
			Collection.Add(item);
	}

	
	public override void OnAdd(object obj)
	{
		using InventoryManagmentEntities _db = new();
		Equipment equipment;
		if (Photo == null)
		{
			string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/AppIcon", "gag.png");
			byte[] photoBytes = File.ReadAllBytes(filePath);
			equipment = new Equipment()
			{
				Photo = photoBytes,
				Name = _name,
				SerialNumber = _serialNumber,
				CategoryID = (int)SelectedCategoryId!,
				Cost = _cost,
				SupplierID = (int)SelectedSupplierId!,
				WarrantyExpiration = _warrantyExpiration,
				PurchaseDate = _purchaseDate,
				StatusID = (int)SelectedStatusId!,
			};
			
		}
		else
		{
			equipment = new Equipment()
			{
				Photo = _photo,
				Name = _name,
				SerialNumber = _serialNumber,
				CategoryID = (int)SelectedCategoryId!,
				Cost = _cost,
				SupplierID = (int)SelectedSupplierId!,
				// SupplierID = 1,
				WarrantyExpiration = _warrantyExpiration,
				PurchaseDate = _purchaseDate,
				StatusID = (int)SelectedStatusId!,
			};
		}
		Collection.Add(equipment);
		_db.Add(equipment);
		_db.SaveChanges();
	}
	
	public async override void LoadData()
	{
		using InventoryManagmentEntities _db = new();
		Collection.Clear();
		await _db.Equipments
			.Include(u => u.Stocks) // Include Stocks navigation property
			.ThenInclude(s => s.Warehouse) // Then include Warehouse from Stock navigation property
			.Include(u => u.Supplier) // Include Supplier navigation property
			.Include(u => u.Category) // Include Category navigation property
			.Include(u => u.Status) // Include Status navigation property
			.Include(u => u.EquipmentMovements) // Include EquipmentMovements navigation property
			.Include(u => u.UtilizationRecords) // Include UtilizationRecords navigation property
			.LoadAsync(); // Asynchronous load of the query
		foreach (var item in _db.Equipments.Local)
		{
			Collection.Add(item);
		}
		OnPropertyChanged(nameof(Collection));
	}



	public List<ImageSource> ImageSources
	{
		get => _imageSources;
		set
		{
			_imageSources = value;
			OnPropertyChanged(nameof(ImageSources));
		}
	}
	
	public string FileName
	{
		get => _fileName;
		set
		{
			_fileName = value;
			OnPropertyChanged(nameof(FileName));
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
	public ObservableCollection<Stock> Stocks
	{
		get => _stocks;
		set
		{
			if (Equals(value, _stocks)) return;
			_stocks = value;
			OnPropertyChanged(nameof(Stocks));
		}
	}
	public Department SelectedFilterDepartment
	{
		get => _selectedFilterDepartment;
		set
		{
			if (_selectedFilterDepartment != value)
			{
				_selectedFilterDepartment = value;
				OnPropertyChanged(nameof(SelectedFilterDepartment));
				OnPropertyChanged(nameof(SelectedDepartmentId));
				OnPropertyChanged(nameof(SelectedFilterDepartment));
				OnPropertyChanged(nameof(SelectedDepartmentId));
			}
		}
	}
	public Supplier SelectedFilterSupplier
	{
		get => _selectedFilterSupplier;
		set
		{
			if (_selectedFilterSupplier != value)
			{
				_selectedFilterSupplier = value;
				OnPropertyChanged(nameof(SelectedFilterSupplier));
				OnPropertyChanged(nameof(SelectedSupplierId));
				OnPropertyChanged(nameof(SelectedFilterSupplier));
				OnPropertyChanged(nameof(SelectedSupplierId));
			}
		}
	}
	public Status SelectedFilterStatus
	{
		get => _selectedFilterStatus;
		set
		{
			if (_selectedFilterStatus != value)
			{
				_selectedFilterStatus = value;
				OnPropertyChanged(nameof(SelectedFilterStatus));
				OnPropertyChanged(nameof(SelectedStatusId));
				OnPropertyChanged(nameof(SelectedFilterStatus));
				OnPropertyChanged(nameof(SelectedStatusId));
				ApplyFilter();
			}
		}
	}
	public Category SelectedFilterCategory
	{
		get => _selectedFilterCategory;
		set
		{
			if (_selectedFilterCategory != value)
			{
				_selectedFilterCategory = value;
				OnPropertyChanged(nameof(SelectedFilterCategory));
				OnPropertyChanged(nameof(SelectedCategoryId));
				OnPropertyChanged(nameof(SelectedFilterCategory));
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
	public ImageSource EquipmentImageSource
	{
		get { return _equipmentImageSource; }
		set { SetProperty(ref _equipmentImageSource, value); }
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
	public string SerialNumber
	{
		get => _serialNumber;
		set
		{
			if (value == _serialNumber) return;
			_serialNumber = value;
			OnPropertyChanged(nameof(SerialNumber));
			OnPropertyChanged(nameof(SerialNumber));
			OnPropertyChanged(nameof(SerialNumber));
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
	public System.DateTime WarrantyExpiration
	{
		get => _warrantyExpiration;
		set
		{
			if (value.Equals(_warrantyExpiration)) return;
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