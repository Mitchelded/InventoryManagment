using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore;
using InventoryManagerMAUI.Commands;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class AdminViewModel : INotifyPropertyChanged
{
    public AdminViewModel() //InventoryManagmentEntities db
    {
        //db = db;
        LoadData();
        DeleteCategoryCommand = new Command<Category>(OnDelete);
        DeleteRoleCommand = new Command<Role>(OnDelete);
        DeleteStatusCommand = new Command<Status>(OnDelete);

        DeleteWarehouseCommand = new Command<Warehouse>(OnDelete);
        DeleteSupplierCommand = new Command<Supplier>(OnDelete);
        DeleteUserCommand = new Command<User>(OnDelete);
        DeleteUserRoleCommand = new Command<UserRole>(OnDelete);


        ResetCommand = new Command(OnReset);
        UpdateCategoryCommand = new Command<Category>(OnUpdate);
        UpdateRoleCommand = new Command<Role>(OnUpdate);
        UpdateStatusCommand = new Command<Status>(OnUpdate);

        UpdateWarehouseCommand = new Command<Warehouse>(OnUpdate);
        UpdateSupplierCommand = new Command<Supplier>(OnUpdate);
        UpdateUserCommand = new Command<User>(OnUpdate);
        UpdateUserRoleCommand = new Command<UserRole>(OnUpdate);


        AddCategoryCommand = new Command<Category>(OnAdd);
        AddRoleCommand = new Command<Role>(OnAdd);
        AddStatusCommand = new Command<Status>(OnAdd);

        AddWarehouseCommand = new Command<Warehouse>(OnAdd);
        AddSupplierCommand = new Command<Supplier>(OnAdd);
        AddUserCommand = new Command<User>(OnAdd);
        AddUserRoleCommand = new Command<UserRole>(OnAdd);
    }

    private void OnAdd(UserRole obj)
    {
        using InventoryManagmentEntities db = new();

        try
        {
            if (SelectedUserItem != null)
            {
                if (SelectedRoleItem != null)
                {
                    UserRole status = new UserRole()
                    {
                        UserID = SelectedUserItem.UserID,
                        RoleID = SelectedRoleItem.RoleID,
                    };

                    CollectionUserRole.Add(status);
                    db.Add(status);
                }
            }

            db.SaveChanges();
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    private void OnAdd(User obj)
    {
        using InventoryManagmentEntities db = new();
        try
        {
            if (SelectedDepartmentItem != null)
            {
                User status = new User()
                {
                    Username = UserName,
                    Password = HashedPassword,
                    Email = Email,
                    DepartmentID = SelectedDepartmentItem.DepartmentID,
                    FullName = FullName,
                    CreatedAt = DateTime.Now
                };

                CollectionUser.Add(status);
                db.Add(status);
            }

            db.SaveChanges();
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    public Department? SelectedDepartmentItem
    {
        get => _selectedDepartmentItem;
        set
        {
            if (Equals(value, _selectedDepartmentItem)) return;
            _selectedDepartmentItem = value;
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public string FullName
    {
        get => _fullName;
        set
        {
            if (value == _fullName) return;
            _fullName = value;
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (value == _email) return;
            _email = value;
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    private string _password;
    private string _hashedPassword;
    public string HashedPassword => _hashedPassword; // Expose hashed password as a read-only property

    public string Password
    {
        get => _password;
        set
        {
            if (value == _password) return;
            _password = value;
            UpdateHashedPasswordAsync(); // Обновить хэш пароля асинхронно
            OnPropertyChanged(nameof(Password)); // Уведомить об изменении пароля
        }
    }

    private async void UpdateHashedPasswordAsync()
    {
        try
        {
            _hashedPassword = await HashHelper.ComputeMD5Hash(_password);
            OnPropertyChanged(nameof(HashedPassword)); // Уведомить об изменении хэша
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    public string UserName
    {
        get => _userName;
        set
        {
            if (value == _userName) return;
            _userName = value;
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    private void OnAdd(Supplier obj)
    {
        using InventoryManagmentEntities db = new();
        try
        {
            if (SelectedCategoryItem != null)
            {
                Supplier status = new Supplier()
                {
                    CompanyName = CompanyName,
                    ContactInfo = ContactInfo,
                    ContactPerson = ContactPerson,
                    Email = Email,
                    Phone = Phone,
                    Website = Website,
                    Address = Address,
                    CategoryID = SelectedCategoryItem.CategoryID,
                };

                CollectionSupplier.Add(status);
                db.Add(status);
            }

            db.SaveChanges();
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    public string? Address
    {
        get => _address;
        set
        {
            if (value == _address) return;
            _address = value;
            OnPropertyChanged();
        }
    }

    public string? Website
    {
        get => _website;
        set
        {
            if (value == _website) return;
            _website = value;
            OnPropertyChanged();
        }
    }

    public string? Phone
    {
        get => _phone;
        set
        {
            if (value == _phone) return;
            _phone = value;
            OnPropertyChanged();
        }
    }

    public string? ContactPerson
    {
        get => _contactPerson;
        set
        {
            if (value == _contactPerson) return;
            _contactPerson = value;
            OnPropertyChanged();
        }
    }

    public string? ContactInfo
    {
        get => _contactInfo;
        set
        {
            if (value == _contactInfo) return;
            _contactInfo = value;
            OnPropertyChanged();
        }
    }

    public string CompanyName
    {
        get => _companyName;
        set
        {
            if (value == _companyName) return;
            _companyName = value;
            OnPropertyChanged();
        }
    }

    private void OnAdd(Warehouse obj)
    {
        using InventoryManagmentEntities db = new();
        try
        {
            Warehouse status = new Warehouse()
            {
                Name = Name,
                Location = Location,
            };

            CollectionWarehouse.Add(status);
            db.Add(status);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    public string Location
    {
        get => _location;
        set
        {
            if (value == _location) return;
            _location = value;
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    private void OnUpdate(UserRole obj)
    {
        if (obj != null)
        {
            Console.WriteLine($"Updating item: {obj}");
            try
            {
                using InventoryManagmentEntities db = new();
                if (SelectedRoleItem != null) obj.RoleID = SelectedRoleItem.RoleID;
                if (SelectedUserItem != null) obj.UserID = SelectedUserItem.UserID;
                db.UserRoles.Update(obj);
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                Console.WriteLine("Item updated successfully");
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
        else
        {
            Console.WriteLine("Item is null, reloading data.");
            LoadData();
        }
    }

    private void OnUpdate(User obj)
    {
        if (obj != null)
        {
            Console.WriteLine($"Updating item: {obj}");
            try
            {
                using InventoryManagmentEntities db = new();
                obj.Username = UserName;
                obj.Password = HashedPassword;
                obj.Email = Email;
                obj.FullName = FullName;
                if (SelectedDepartmentItem != null) obj.DepartmentID = SelectedDepartmentItem.DepartmentID;
                db.Users.Update(obj);
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                Console.WriteLine("Item updated successfully");
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
        else
        {
            Console.WriteLine("Item is null, reloading data.");
            LoadData();
        }
    }

    private void OnUpdate(Supplier obj)
    {
        if (obj != null)
        {
            Console.WriteLine($"Updating item: {obj}");
            try
            {
                using InventoryManagmentEntities db = new();
                obj.CompanyName = Name;
                obj.ContactInfo = ContactInfo;
                obj.ContactPerson = ContactPerson;
                obj.Email = Email;
                obj.Phone = Phone;
                obj.Website = Website;
                obj.Address = Address;
                if (SelectedCategoryItem != null) obj.CategoryID = SelectedCategoryItem.CategoryID;
                db.Suppliers.Update(obj);
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                Console.WriteLine("Item updated successfully");
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
        else
        {
            Console.WriteLine("Item is null, reloading data.");
            LoadData();
        }
    }

    private void OnUpdate(Warehouse obj)
    {
        if (obj != null)
        {
            Console.WriteLine($"Updating item: {obj}");
            try
            {
                using InventoryManagmentEntities db = new();
                obj.Name = Name;
                obj.Location = Location;
                db.Warehouses.Update(obj);
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                Console.WriteLine("Item updated successfully");
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
        else
        {
            Console.WriteLine("Item is null, reloading data.");
            LoadData();
        }
    }

    private void OnDelete(UserRole obj)
    {
        if (obj != null)
        {
            try
            {
                using InventoryManagmentEntities db = new();
                CollectionUserRole.Remove(obj);
                db.Set<UserRole>().Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
    }

    private void OnDelete(User obj)
    {
        if (obj != null)
        {
            try
            {
                using InventoryManagmentEntities db = new();
                CollectionUser.Remove(obj);
                db.Set<User>().Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
    }

    private void OnDelete(Supplier obj)
    {
        if (obj != null)
        {
            try
            {
                using InventoryManagmentEntities db = new();
                CollectionSupplier.Remove(obj);
                db.Set<Supplier>().Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
    }

    private void OnDelete(Warehouse obj)
    {
        if (obj != null)
        {
            try
            {
                using InventoryManagmentEntities db = new();
                CollectionWarehouse.Remove(obj);
                db.Set<Warehouse>().Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
    }

    private void OnAdd(Status obj)
    {
        using InventoryManagmentEntities db = new();
        try
        {
            Status status = new Status()
            {
                Name = Name,
                Description = Description,
            };

            CollectionStatus.Add(status);
            db.Add(status);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    private void OnAdd(Role obj)
    {
        using InventoryManagmentEntities db = new();
        try
        {
            Role role = new Role()
            {
                Name = Name,
                Description = Description,
            };

            CollectionRole.Add(role);
            db.Add(role);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    private void OnAdd(Category obj)
    {
        using InventoryManagmentEntities db = new();
        try
        {
            Category category = new Category()
            {
                Name = Name,
                Description = Description,
            };

            CollectionCategory.Add(category);
            db.Add(category);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (value == _description) return;
            _description = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged(Description);
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged(Name);
        }
    }

    private void OnUpdate(Status obj)
    {
        if (obj != null)
        {
            Console.WriteLine($"Updating item: {obj}");
            try
            {
                using InventoryManagmentEntities db = new();
                obj.Name = Name;
                obj.Description = Description;
                db.Statuses.Update(obj);
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                Console.WriteLine("Item updated successfully");
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
        else
        {
            Console.WriteLine("Item is null, reloading data.");
            LoadData();
        }
    }

    private void OnUpdate(Role obj)
    {
        if (obj != null)
        {
            Console.WriteLine($"Updating item: {obj}");
            try
            {
                using InventoryManagmentEntities db = new();
                obj.Name = Name;
                obj.Description = Description;
                db.Roles.Update(obj);
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                Console.WriteLine("Item updated successfully");
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
        else
        {
            Console.WriteLine("Item is null, reloading data.");
            LoadData();
        }
    }

    private void OnUpdate(Category obj)
    {
        if (obj != null)
        {
            Console.WriteLine($"Updating item: {obj}");
            try
            {
                using InventoryManagmentEntities db = new();
                obj.Name = Name;
                obj.Description = Description;
                db.Category.Update(obj);
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                Console.WriteLine("Item updated successfully");
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
        else
        {
            Console.WriteLine("Item is null, reloading data.");
            LoadData();
        }
    }

    private void OnReset()
    {
        LoadData();
    }

    private void LoadData()
    {
        using InventoryManagmentEntities db = new();
        try
        {
            CollectionCategory.Clear();
            CollectionRole.Clear();
            CollectionStatus.Clear();
            CollectionWarehouse.Clear();
            CollectionSupplier.Clear();
            CollectionUser.Clear();
            CollectionUserRole.Clear();
            CollectionDepartment.Clear();

            db.Set<Category>().Load();
            db.Set<Role>().Load();
            db.Set<Status>().Load();
            db.Set<Warehouse>().Load();
            db.Set<Supplier>().Load();
            db.Set<User>().Load();
            db.Set<UserRole>().Load();
            db.Set<Department>().Load();
            foreach (var item in db.Set<Department>().Local)
            {
                CollectionDepartment.Add(item);
            }

            foreach (var item in db.Set<Category>().Local)
            {
                CollectionCategory.Add(item);
            }

            foreach (var item in db.Set<Role>().Local)
            {
                CollectionRole.Add(item);
            }

            foreach (var item in db.Set<Status>().Local)
            {
                CollectionStatus.Add(item);
            }

            foreach (var item in db.Set<Warehouse>().Local)
            {
                CollectionWarehouse.Add(item);
            }

            foreach (var item in db.Set<Supplier>().Local)
            {
                CollectionSupplier.Add(item);
            }

            foreach (var item in db.Set<User>().Local)
            {
                CollectionUser.Add(item);
            }

            foreach (var item in db.Set<UserRole>().Local)
            {
                CollectionUserRole.Add(item);
            }

            OnPropertyChanged(nameof(CollectionCategory));
            OnPropertyChanged(nameof(CollectionRole));
            OnPropertyChanged(nameof(CollectionStatus));
            OnPropertyChanged(nameof(CollectionWarehouse));
            OnPropertyChanged(nameof(CollectionSupplier));
            OnPropertyChanged(nameof(CollectionUser));
            OnPropertyChanged(nameof(CollectionUserRole));
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    public ObservableCollection<Department> CollectionDepartment
    {
        get => _collectionDepartment;
        set
        {
            if (Equals(value, _collectionDepartment)) return;
            _collectionDepartment = value;
            OnPropertyChanged();
        }
    }

    private void OnDelete(Status obj)
    {
        if (obj != null)
        {
            try
            {
                using InventoryManagmentEntities db = new();
                CollectionStatus.Remove(obj);
                db.Set<Status>().Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");

            }
        }
    }

    private void OnDelete(Role obj)
    {
        if (obj != null)
        {
            try
            {
                using InventoryManagmentEntities db = new();
                CollectionRole.Remove(obj);
                db.Set<Role>().Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");

            }
        }
    }

    private void OnDelete(Category obj)
    {
        if (obj != null)
        {
            try
            {
                using InventoryManagmentEntities db = new();
                CollectionCategory.Remove(obj);
                db.Set<Category>().Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");

            }
        }
    }


    private ObservableCollection<Category> _collectionCategory = new();
    private ObservableCollection<Role> _collectionRole = new();
    private ObservableCollection<Status> _collectionStatus = new();

    private ObservableCollection<Warehouse> _collectionWarehouse = new();

    public ObservableCollection<Warehouse> CollectionWarehouse
    {
        get => _collectionWarehouse;
        set
        {
            if (Equals(value, _collectionWarehouse)) return;
            _collectionWarehouse = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Supplier> CollectionSupplier
    {
        get => _collectionSupplier;
        set
        {
            if (Equals(value, _collectionSupplier)) return;
            _collectionSupplier = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public ObservableCollection<User> CollectionUser
    {
        get => _collectionUser;
        set
        {
            if (Equals(value, _collectionUser)) return;
            _collectionUser = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public ObservableCollection<UserRole> CollectionUserRole
    {
        get => _collectionUserRole;
        set
        {
            if (Equals(value, _collectionUserRole)) return;
            _collectionUserRole = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Supplier> _collectionSupplier = new();
    private ObservableCollection<User> _collectionUser = new();
    private ObservableCollection<UserRole> _collectionUserRole = new();
    private Category? _selectedCategoryItem;

    private Warehouse? _selectedWarehouseItem;

    public Warehouse? SelectedWarehouseItem
    {
        get => _selectedWarehouseItem;
        set
        {
            if (Equals(value, _selectedWarehouseItem)) return;
            _selectedWarehouseItem = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public Supplier? SelectedSupplierItem
    {
        get => _selectedSupplierItem;
        set
        {
            if (Equals(value, _selectedSupplierItem)) return;
            _selectedSupplierItem = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public User? SelectedUserItem
    {
        get => _selectedUserItem;
        set
        {
            if (Equals(value, _selectedUserItem)) return;
            _selectedUserItem = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public UserRole? SelectedUserRoleItem
    {
        get => _selectedUserRoleItem;
        set
        {
            if (Equals(value, _selectedUserRoleItem)) return;
            _selectedUserRoleItem = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    private Supplier? _selectedSupplierItem;
    private User? _selectedUserItem;
    private UserRole? _selectedUserRoleItem;

    public Category? SelectedCategoryItem
    {
        get => _selectedCategoryItem;
        set
        {
            if (Equals(value, _selectedCategoryItem)) return;
            _selectedCategoryItem = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public Role? SelectedRoleItem
    {
        get => _selectedRoleItem;
        set
        {
            if (Equals(value, _selectedRoleItem)) return;
            _selectedRoleItem = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public Status? SelectedStatusItem
    {
        get => _selectedStatusItem;
        set
        {
            if (Equals(value, _selectedStatusItem)) return;
            _selectedStatusItem = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    private Role? _selectedRoleItem;
    private Status? _selectedStatusItem;
    private string _name;
    private string _description;
    private Department? _selectedDepartmentItem;
    private string _fullName;
    private string _email;
    private string _userName;
    private string _location;
    private string? _address;
    private string? _website;
    private string? _phone;
    private string? _contactPerson;
    private string? _contactInfo;
    private string _companyName;
    private ObservableCollection<Department> _collectionDepartment = new();

    public ObservableCollection<Category> CollectionCategory
    {
        get => _collectionCategory;
        private set
        {
            if (Equals(value, _collectionCategory)) return;
            _collectionCategory = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Role> CollectionRole
    {
        get => _collectionRole;
        private set
        {
            if (Equals(value, _collectionRole)) return;
            _collectionRole = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Status> CollectionStatus
    {
        get => _collectionStatus;
        private set
        {
            if (Equals(value, _collectionStatus)) return;
            _collectionStatus = value;
            OnPropertyChanged();
            OnPropertyChanged();
            OnPropertyChanged();
        }
    }

    public ICommand DeleteCategoryCommand { get; }
    public ICommand DeleteRoleCommand { get; }
    public ICommand DeleteStatusCommand { get; }

    public ICommand DeleteWarehouseCommand { get; }
    public ICommand DeleteSupplierCommand { get; }
    public ICommand DeleteUserCommand { get; }
    public ICommand DeleteUserRoleCommand { get; }


    public ICommand UpdateCategoryCommand { get; }
    public ICommand UpdateRoleCommand { get; }
    public ICommand UpdateStatusCommand { get; }

    public ICommand UpdateWarehouseCommand { get; }
    public ICommand UpdateSupplierCommand { get; }
    public ICommand UpdateUserCommand { get; }
    public ICommand UpdateUserRoleCommand { get; }

    public ICommand AddCategoryCommand { get; }
    public ICommand AddRoleCommand { get; }
    public ICommand AddStatusCommand { get; }

    public ICommand AddWarehouseCommand { get; }
    public ICommand AddSupplierCommand { get; }
    public ICommand AddUserCommand { get; }
    public ICommand AddUserRoleCommand { get; }

    public ICommand ResetCommand { get; }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}