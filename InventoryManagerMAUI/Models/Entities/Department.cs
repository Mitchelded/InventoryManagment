namespace InventoryManagment.Models.Entities;

public class Department
{
    public int DepartmentID { get; set; }
    public string Name { get; set; }
    public string HeadOfDepartment { get; set; }

    public ICollection<User> Users { get; set; }
}