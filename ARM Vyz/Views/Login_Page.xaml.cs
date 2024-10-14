using ARM_Vyz.Commands;
using ARM_Vyz.Model.Entities;
using ARM_Vyz.Model;
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
	/// Логика взаимодействия для Login_Page.xaml
	/// </summary>
	public partial class Login_Page : Page
	{
		public Login_Page()
		{
			InitializeComponent();
		}

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			using (UniversityEntities _db = new UniversityEntities())
			{

				EcryptMethodes ecryptMethodes = new EcryptMethodes();
				string encryptPass = ecryptMethodes.Ecrypt(pbPassword.Password);
				People people = _db.People.Include("Roles").Single(x=> x.Password == encryptPass
				&&  x.Login==tbLogin.Text);

				
				if(people != null)
				{
					switch (people.Roles.RoleName)
					{
						case "Dean": this.NavigationService.Navigate(new ShowStudents()); break;
					}

				}
			}
		}
	}
}
