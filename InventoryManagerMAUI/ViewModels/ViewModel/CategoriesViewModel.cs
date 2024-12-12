using System.Collections.ObjectModel;
using System.Windows.Input;
using InventoryManagerMAUI.Interface;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class CategoryViewModel : ViewModelBase<Category>
{

	public CategoryViewModel() : base()
	{

	}

	public override void OnAdd(object obj)
	{
		using InventoryManagmentEntities _db = new();
		var Category = new Category()
		{

			Name = _name,
			Description = _description,
		};
		Collection.Add(Category);
		_db.Add(Category);
		_db.SaveChanges();
	}

	private int _idCategory;
	private string _name;
	private string _description;


	public int IdCategory
	{
		get => _idCategory;
		set
		{
			if (value == _idCategory) return;
			_idCategory = value;
			OnPropertyChanged(nameof(IdCategory));
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