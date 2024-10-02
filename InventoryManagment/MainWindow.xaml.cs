using InventoryManagment.Models;
using System.Collections.ObjectModel;
using System.Windows;
using InventoryManagment.Commands.DbMethods;

namespace InventoryManagment
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Employees> Employees = new ObservableCollection<Employees>()
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
                EquipmentMethods equipmentMethods = new();
                var q = equipmentMethods.GetAll();


                // ls.ItemsSource = employees;

                //dg.ItemsSource = q.ToList();
            }
        }

        // private void packIconEdit_MouseDown(object sender, MouseButtonEventArgs e)
        // {
        //     var employeeEdit = GetInstancesView.GetInstanceView<Employees>(sender);
        //     var employeeToEdit = employees.Single(x => x.IdEmployee == employeeEdit.IdEmployee);
        //
        //     employeeToEdit.Position = employeeEdit.Position;
        //     employeeToEdit.LastName = employeeEdit.LastName;
        //     employeeToEdit.FirstName = employeeEdit.FirstName;
        //     employeeToEdit.DepartmentID = employeeEdit.DepartmentID;
        //     ls.Items.Refresh();
        // }
        //
        // private void packIconRemove_MouseDown(object sender, MouseButtonEventArgs e)
        // {
        //     var employeeToRemove = GetInstancesView.GetInstanceView<Employees>(sender);
        //     employees.Remove(employeeToRemove);
        //     ls.Items.Refresh();
        // }
    }
}