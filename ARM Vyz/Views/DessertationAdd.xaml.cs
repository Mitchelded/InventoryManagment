using ARM_Vyz.Model.Entities;
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
using System.Windows.Shapes;

namespace ARM_Vyz.Views
{
	/// <summary>
	/// Логика взаимодействия для DessertationAdd.xaml
	/// </summary>
	/// 


	public partial class DessertationAdd : Window
	{
		public List<Dessertations> Dessertations = new List<Dessertations>();


		public DessertationAdd()
		{
			InitializeComponent();

		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			Dessertations dessertation = new Dessertations()
			{
				NameDissertation = tbName.Text,
				Year = dpYear.SelectedDate,
				//TeacherID = _people.PeopleID,
			};

			Dessertations.Add(dessertation);

			MessageBox.Show("Данные записаны");
        }

		private void Window_Closed(object sender, EventArgs e)
		{

		}
	}
}
