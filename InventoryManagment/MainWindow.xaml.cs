
using InventoryManagment.Commands;
using InventoryManagment.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		public ObservableCollection<Employees> employees = new ObservableCollection<Employees>()
				{
				new Employees()
				{
					IdEmployee = 1,
									FirstName = "FirstName",
									LastName = "LastName",
									Position = "Position",
									DepartmentID = 1,
								},
				new Employees()
				{
					IdEmployee = 2,
									FirstName = "FirstName2",
									LastName = "LastName2",
									Position = "Position2",
									DepartmentID = 2,
								},
				};
		public MainWindow()
		{
			InitializeComponent();


			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				var q = EquipmentMethods.GetAllEquipments();


				ls.ItemsSource = employees;

				//dg.ItemsSource = q.ToList();
			}


		}

		private T GetInstanceView<T>(object sender) where T : class
		{
			var packIcon = sender as PackIcon;
			if (packIcon != null)
			{
				var employeeEdit = packIcon.DataContext as T;
				return employeeEdit; // Возвращаем DataContext, приведенный к T
			}
			return default; // Возвращаем значение по умолчанию для T, если packIcon null
		}



		private void packIconEdit_MouseDown(object sender, MouseButtonEventArgs e)
		{
			var employeeEdit = GetInstanceView<Employees>(sender);
			var employeeToEdit = employees.Single(x => x.IdEmployee == employeeEdit.IdEmployee);

			employeeToEdit.Position = employeeEdit.Position;
			employeeToEdit.LastName = employeeEdit.LastName;
			employeeToEdit.FirstName = employeeEdit.FirstName;
			employeeToEdit.DepartmentID = employeeEdit.DepartmentID;
			ls.Items.Refresh();
		}

		private void packIconRemove_MouseDown(object sender, MouseButtonEventArgs e)
		{
			var employeeToRemove = GetInstanceView<Employees>(sender);
			employees.Remove(employeeToRemove);
			ls.Items.Refresh();
		}
	}
}
