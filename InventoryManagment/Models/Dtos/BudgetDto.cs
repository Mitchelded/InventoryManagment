using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Models.Dtos
{
	internal class BudgetDto
	{
		public int IDBudget { get; set; }
		public string AllocationDate { get; set; }
		public string Name { get; set; }
		public string HeadOfDepartment { get; set; }
		public string Amount { get; set; }
		public string Purpose { get; set; }
	}
}
