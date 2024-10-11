using ARM_Vyz.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ARM_Vyz.Views
{
	/// <summary>
	/// Логика взаимодействия для Registration_Page.xaml
	/// </summary>
	public partial class Registration_Page : Page
	{
		public Registration_Page()
		{
			InitializeComponent();
			using (UniversityEntities _db = new UniversityEntities())
			{
				People people = new People()
				{
					RoleID = 1,	
					FIO = "FIO",
					Birthday = DateTime.Now,
					HaveAChild = true,
					Scholarship = 1000,
					Gender = "Man",
					Salary = 1000,
					DepartmentID = 1,
				};
				people.AddDessertation(
					new ScientificDegrees()
					{
						NameDissertation = "NameDissertation",
						Year = DateTime.Now,
						TeacherID = 1,
					});

				_db.People.Add(people);
				_db.SaveChanges();
			}
		}
	}
}
