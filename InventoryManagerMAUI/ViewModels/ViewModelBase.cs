using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagerMAUI.ViewModels;

public class ViewModelBase<T> : INotifyPropertyChanged where T : class
{
	//protected readonly InventoryManagmentEntities _db;
	public ObservableCollection<T> Collection { get; private set; } = new ObservableCollection<T>();
	public ICommand DeleteCommand { get; }
	public ICommand UpdateCommand { get; }
	public ICommand AddCommand { get; }
	public ICommand ResetCommand { get; }
	
	public ViewModelBase() //InventoryManagmentEntities db
	{
		//_db = db;
		LoadData();
		DeleteCommand = new Command<T>(OnDelete);
		ResetCommand = new Command<T>(OnResete);
		UpdateCommand = new Command<T>(OnUpdate);
		AddCommand = new Command(OnAdd);
	}

	public virtual void OnResete(T obj)
	{
		LoadData();
	}


	private T? _selectedItem;

	public T? SelectedItem
	{
		get => _selectedItem;
		set
		{ _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }

	}

	public virtual void OnAdd(object obj)
	{
		// Logic for adding a new item
	}

	public virtual async Task LoadData()
	{
		using InventoryManagmentEntities _db = new();
		Collection.Clear();
		await _db.Set<T>()

			.LoadAsync();
		foreach (var item in _db.Set<T>().Local)
		{
			Collection.Add(item);
		}
		OnPropertyChanged(nameof(Collection));
	}

	public virtual async void OnUpdate(T item)
	{
		if (item != null)
		{
			Console.WriteLine($"Updating item: {item}");
			try
			{
				using InventoryManagmentEntities _db = new();
				_db.Entry(item).State = EntityState.Modified;
				_db.SaveChanges();
				Console.WriteLine("Item updated successfully");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating item: {ex.Message}");
			}
		}
		else
		{
			Console.WriteLine("Item is null, reloading data.");
			LoadData();
		}
	}



	public virtual void OnDelete(T item)
	{
		if (item != null)
		{
			try
			{
				using InventoryManagmentEntities _db = new();
				Collection.Remove(item);
				_db.Set<T>().Remove(item);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				// Handle the error (e.g., log it, notify user)
			}
		}
	}
	protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(backingField, value))
			return false;

		backingField = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	public event PropertyChangedEventHandler? PropertyChanged;

	protected void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
