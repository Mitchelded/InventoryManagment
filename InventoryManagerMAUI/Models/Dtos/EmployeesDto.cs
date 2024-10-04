namespace InventoryManagment.Models.Dtos
{
	internal class EmployeesDto
	{
		public int IdEmployee { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string Position { get; set; }
		public string NameDepartment { get; set; }
		public string HeadOfDepartment { get; set; }

		public string Location { get; set; }
	}
}
