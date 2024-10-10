﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagerMAUI.ViewModels;

public class ViewModelBase<T> : INotifyPropertyChanged where T : class
{
	//protected readonly InventoryManagmentEntities _db;
	public ObservableCollection<T> Collection { get; private set; } = new ObservableCollection<T>();
	public ICommand DeleteCommand { get; }
	public ICommand UpdateCommand { get; }
	public ICommand AddCommand { get; }

	
	public ViewModelBase() //InventoryManagmentEntities db
	{
		//_db = db;
		LoadData();
		DeleteCommand = new Command<T>(OnDelete);
		UpdateCommand = new Command<T>(OnUpdate);
		AddCommand = new Command(OnAdd);
	}

	public virtual void OnAdd(object obj)
	{
		// Logic for adding a new item
	}

	public async void LoadData()
	{
		using InventoryManagmentEntities _db = new();
		Collection.Clear();
		await _db.Set<T>().LoadAsync();
		foreach (var item in _db.Set<T>().Local)
		{
			Collection.Add(item);
		}
		OnPropertyChanged(nameof(Collection));
	}

	public virtual void OnUpdate(T item)
	{
		if (item != null)
		{
			try
			{
				using InventoryManagmentEntities _db = new();
				_db.Entry(item).State = EntityState.Modified;
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				// Handle the error (e.g., log it, notify user)
			}
		}
		else
		{
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

	public event PropertyChangedEventHandler? PropertyChanged;

	protected void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
