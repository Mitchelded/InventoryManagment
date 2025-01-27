﻿using ARM_Vyz.Model;
using ARM_Vyz.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ARM_Vyz.Commands;
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
				cbRole.ItemsSource = _db.Roles.ToList();
				cbDepartment.ItemsSource = _db.Departments.ToList();
			}
		}

		private People _people;
		private List<Dessertations> _dessertations;
		private async void btnReg_Click(object sender, RoutedEventArgs e)
		{
			using (UniversityEntities _db = new UniversityEntities())
			{
				try
				{
					if (tbPassword.Password == tbRepPassword.Password)
					{
						EcryptMethodes ecryptMethodes = new EcryptMethodes();
						var people = new People()
						{
							RoleID = int.Parse(cbRole.SelectedValue.ToString()),
							FIO = tbFIO.Text,
							Birthday = (DateTime)dpBirthDay.SelectedDate,
							HaveAChild = cbChild.IsChecked ?? false,
							Scholarship = decimal.Parse(tbSholarship.Text),
							Gender = ((ComboBoxItem)cbGender.SelectedItem).Content.ToString(),
							Salary = decimal.Parse(tbSalary.Text),
							Login = tbLogin.Text,
							Password = ecryptMethodes.Ecrypt(tbPassword.Password),
							Approved = false,
							DepartmentID =int.Parse(cbDepartment.SelectedValue.ToString()),
						};
						if (_db.People.FirstOrDefault(x => x.Login == people.Login) == null)
						{
							_db.People.Add(people);
							_db.SaveChanges(); // Save before using the ID
							if (_dessertations != null)
							{
								foreach (var dessertation in _dessertations)
								{
									dessertation.TeacherID = people.PeopleID; // Use the original reference
								}

								_db.Dessertations.AddRange(_dessertations);
								await _db.SaveChangesAsync(); // Use async save if in a UI context
							}
						}
						else
						{
							MessageBox.Show("User already exist");
						}
					}
				}

				catch (DbEntityValidationException ex)
				{
					foreach (var eve in ex.EntityValidationErrors)
					{
						Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
							eve.Entry.Entity.GetType().Name, eve.Entry.State);
						foreach (var ve in eve.ValidationErrors)
						{
							Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
								ve.PropertyName, ve.ErrorMessage);
						}
					}
					throw;
				}
			}
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			DessertationAdd dessertationAdd = new DessertationAdd();
			dessertationAdd.ShowDialog();

			_dessertations = dessertationAdd.Dessertations;
		}

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			this.NavigationService.Navigate(new Login_Page());
		}
	}

}


