
using InventoryManagment.Commands;
using InventoryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagment
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();


			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				var q = EquipmentMethods.GetAllEquipmets();
				#region first_variant
				var equipments = from equipment in db.Equipments
								 join supplier in db.Suppliers on equipment.SupplierID equals supplier.IdSuppliers
								 join department in db.Departments on equipment.DepartmentID equals department.IdDepartments
								 join category in db.Categories on equipment.CategoryID equals category.IdCategories
								 join location in db.Locations on equipment.LocationID equals location.IdLocations
								 select new
								 {
									 Name = equipment.Name,
									 Serial_Number = equipment.Serial_Number,
									 Category = category.Name,
									 Department = department.Name,
									 Location = location.Description,
									 Supplier = supplier.Name,
									
								 };
				#endregion

				#region second_variant:
				//var equipments2 = db.Equipments
				//.Join(db.Suppliers,
				//	  equipment => equipment.SupplierID,
				//	  supplier => supplier.IdSuppliers,
				//	  (equipment, supplier) => new { equipment, supplier })
				//.Join(db.Departments,
				//	  es => es.equipment.DepartmentID,
				//	  department => department.IdDepartments,
				//	  (es, department) => new
				//	  {
				//		  es.equipment,
				//		  es.supplier,
				//		  department
				//	  })
				//.Select(e => new
				//{
				//	e.equipment.IdEquipment,
				//	e.equipment.Name,
				//	SupplierName = e.supplier.Name,
				//	DepartmentName = e.department.Name
				//});
				#endregion

				dg.ItemsSource = equipments.ToList();
			}


		}
	}
}
