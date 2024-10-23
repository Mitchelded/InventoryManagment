using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels.ViewModel
{
	class EmployeesViewModel : ViewModelBase<Employees>
	{
		private int _idEmployee;
		private string _firstName;
		private string _lastName;
		private string _patronymic;
		private string _position;
		private int _departmentId;

		public int IdEmployee
		{
			get => _idEmployee;
			set
			{
				if (value == _idEmployee) return;
				_idEmployee = value;
				OnPropertyChanged(nameof(IdEmployee));
			}
		}

		public string FirstName
		{
			get => _firstName;
			set
			{
				if (value == _firstName) return;
				_firstName = value;
				OnPropertyChanged(nameof(FirstName));
			}
		}

		public string LastName
		{
			get => _lastName;
			set
			{
				if (value == _lastName) return;
				_lastName = value;
				OnPropertyChanged(nameof(LastName));
			}
		}

		public string Patronymic
		{
			get => _patronymic;
			set
			{
				if (value == _patronymic) return;
				_patronymic = value;
				OnPropertyChanged(nameof(Patronymic));
			}
		} // Optional: consider making this nullable

		public string Position
		{
			get => _position;
			set
			{
				if (value == _position) return;
				_position = value;
				OnPropertyChanged(nameof(Position));
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
			}
		}

		public EmployeesViewModel() : base()
		{

		}


		public override void OnAdd(object obj)
		{
			using InventoryManagmentEntities _db = new();
			var status = new Employees()
			{
				IdEmployee = _idEmployee,
				FirstName = _firstName,
				LastName = _lastName,
				Patronymic = _patronymic,
				Position = _position,
				DepartmentID = _departmentId,
            
			};
			Collection.Add(status);
			_db.Add(status);
			_db.SaveChanges();
		}
	}
}
