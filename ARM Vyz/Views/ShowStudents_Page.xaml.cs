using ARM_Vyz.Model;
using ARM_Vyz.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ARM_Vyz.Views
{
	/// <summary>
	/// Логика взаимодействия для ShowStudents_Page.xaml
	/// </summary>
	public partial class ShowStudents_Page : Page
	{
		public ShowStudents_Page()
		{

			InitializeComponent();

			Refresh();
			using (UniversityEntities db = new UniversityEntities())
			{
				var faculties = db.Faculties.ToList();
				var departments = db.Departments.ToList();
				var course = db.People
					.Select(x => x.Course)
					.Distinct()
					.ToList();

				cbFaculty.DataContext = faculties;
				cbFaculty.ItemsSource = faculties;


				cbCourse.DataContext = course;
				cbCourse.ItemsSource = course;


				cbDepartment.DataContext = departments;
				cbDepartment.ItemsSource = departments;
			}
		}
		ObservableCollection<People> peoples = new ObservableCollection<People>();
		private void Refresh()
		{
			using (UniversityEntities db = new UniversityEntities())
			{
				peoples = new ObservableCollection<People>(
					db.People.Include("Departments")
					.Include("Faculties")
					.Include("Roles")
					.Where(x=>x.Roles.RoleName == "Student")
					.ToList());
				lvStudents.ItemsSource = peoples;
				DataContext = peoples;
				cbFaculties.DataContext = db.Faculties.ToList();
				cbDepartments.DataContext = db.Departments.ToList();
			}
		}

		private void miDelete_Click(object sender, RoutedEventArgs e)
		{
			var menuItem = sender as MenuItem;

			var people = menuItem.DataContext as People;

			using (UniversityEntities db = new UniversityEntities())
			{
				// TODO: проверить при введеных Department и Faculty
				db.People.Remove(db.People.FirstOrDefault(x => x.PeopleID == people.PeopleID));
				db.SaveChanges();
				Refresh(); // Call your refresh method to update UI
			}
		}

		private void miChange_Click(object sender, RoutedEventArgs e)
		{
			using (UniversityEntities db = new UniversityEntities())
			{

				cbDepartments.ItemsSource = new
					ObservableCollection<Departments>(db.Departments.ToList());
				cbFaculties.ItemsSource = new ObservableCollection<Faculties>(db.Faculties.ToList());
			}

			var menuItem = sender as MenuItem;

			var people = menuItem.DataContext as People;
			popup.DataContext = people;
			popup.IsOpen = true;
		}

		public static class MyVisualTreeHelper
		{
			static bool AlwaysTrue<T>(T obj) { return true; }

			/// <summary>
			/// Finds a parent of a given item on the visual tree. If the element is a ContentElement or FrameworkElement 
			/// it will use the logical tree to jump the gap.
			/// If not matching item can be found, a null reference is returned.
			/// </summary>
			/// <typeparam name="T">The type of the element to be found</typeparam>
			/// <param name="child">A direct or indirect child of the wanted item.</param>
			/// <returns>The first parent item that matches the submitted type parameter. If not matching item can be found, a null reference is returned.</returns>
			public static T FindParent<T>(DependencyObject child) where T : DependencyObject
			{
				return FindParent<T>(child, AlwaysTrue<T>);
			}

			public static T FindParent<T>(DependencyObject child, Predicate<T> predicate) where T : DependencyObject
			{
				DependencyObject parent = GetParent(child);
				if (parent == null)
					return null;

				// check if the parent matches the type and predicate we're looking for
				if ((parent is T) && (predicate((T)parent)))
					return parent as T;
				else
					return FindParent<T>(parent);
			}

			static DependencyObject GetParent(DependencyObject child)
			{
				DependencyObject parent = null;
				if (child is Visual || child is Visual3D)
					parent = VisualTreeHelper.GetParent(child);

				// if fails to find a parent via the visual tree, try to logical tree.
				return parent ?? LogicalTreeHelper.GetParent(child);
			}
		}

		void OnListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.Handled)
				return;

			ListViewItem item = MyVisualTreeHelper.FindParent<ListViewItem>((DependencyObject)e.OriginalSource);
			if (item == null)
				return;

			if (item.Focusable && !item.IsFocused)
				item.Focus();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			popup.IsOpen = false;
		}

		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			var btn = sender as Button;
			var personToUpdate = btn.DataContext as People;

			if (personToUpdate == null) return; // Ensure person data context is valid

			using (UniversityEntities db = new UniversityEntities())
			{
				// Retrieve the existing person from the database
				var existingPerson = db.People.FirstOrDefault(x => x.PeopleID == personToUpdate.PeopleID);
				if (existingPerson != null)
				{
					// Update properties of the existing person with new values
					existingPerson.FIO = personToUpdate.FIO;
					existingPerson.Gender = personToUpdate.Gender;
					existingPerson.Birthday = personToUpdate.Birthday;
					existingPerson.HaveAChild = personToUpdate.HaveAChild;
					existingPerson.Scholarship = personToUpdate.Scholarship;
					existingPerson.ScholarshipPurpose = personToUpdate.ScholarshipPurpose;

					// Save changes to the database
					db.SaveChanges();
					Refresh(); // Call your refresh method to update UI
				}
			}

			popup.IsOpen = false; // Close the popup
		}

		private void cbSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var comboBox = sender as ComboBox;
			if (comboBox.SelectedItem == null) return;

			using (UniversityEntities db = new UniversityEntities())
			{
				var selectedItem = comboBox.SelectedItem;

				// Метод для получения студентов по фильтру
				peoples = new ObservableCollection<People>(
					GetFilteredPeople(db, selectedItem));

				// Обновите источник данных
				lvStudents.ItemsSource = peoples;
			}
		}

		private IEnumerable<People> GetFilteredPeople(UniversityEntities db, object filter)
		{
			IQueryable<People> query = db.People.Include("Departments").Include("Faculties").AsNoTracking();

			if (filter is Faculties faculty)
			{
				query = query.Where(x => x.Departments.Faculties.FacultyName == faculty.FacultyName);
			}
			else if (filter is Departments department)
			{
				query = query.Where(x => x.Departments.DepartmentName == department.DepartmentName);
			}
			else if (filter is string course)
			{
				query = query.Where(x => x.Course == course);
			}

			return query.ToList();
		}


		private void Approve_CLick(object sender, RoutedEventArgs e)
		{
			var menuItem = sender as MenuItem;
			var personToUpdate = menuItem.DataContext as People;

			if (personToUpdate == null) return; // Ensure person data context is valid

			using (UniversityEntities db = new UniversityEntities())
			{
				// Retrieve the existing person from the database
				var existingPerson = db.People.FirstOrDefault(x => x.PeopleID == personToUpdate.PeopleID);
				if (existingPerson != null)
				{
					// Update properties of the existing person with new values
					existingPerson.Approved = !personToUpdate.Approved;

					// Save changes to the database
					db.SaveChanges();
					Refresh(); // Call your refresh method to update UI
				}
			}
		}

		private void BtnReset_OnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
