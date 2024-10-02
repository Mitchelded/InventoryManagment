namespace InventoryManagment.Models.Dtos
{
	internal class BudgetDto
	{
		public int IdBudget { get; set; }
		public string AllocationDate { get; set; }
		public string NameDepartment { get; set; }
		public string HeadOfDepartment { get; set; }
		public string Amount { get; set; }
		public string Purpose { get; set; }
	}
}
