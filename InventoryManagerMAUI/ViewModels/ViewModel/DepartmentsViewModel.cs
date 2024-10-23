using InventoryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerMAUI.ViewModels.ViewModel
{
	class DepartmentsViewModel : ViewModelBase<Departments>
	{
		public DepartmentsViewModel() : base()
		{

		}
		private string _name;
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

		private string _headOfDepartment;
		public string HeadOfDepartment
		{
			get => _name;
			set
			{
				if (value == _name) return;
				_name = value;
				OnPropertyChanged(nameof(HeadOfDepartment));
			}
		}

		public override void OnAdd(object obj)
		{
			using InventoryManagmentEntities _db = new();
			var status = new Departments()
			{
				Name = _name,
				HeadOfDepartment = _headOfDepartment,
			};

			var q = _db.Departments.Add(status);
			Collection.Add(status);
			_db.SaveChanges();
		}
	}
}
