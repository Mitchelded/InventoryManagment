using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Models.Dtos
{
	internal class WarantyDto
	{
		public int IdWarranty { get; set; }
		public string NameEquipment { get; set; }
		public string Serial_Number { get; set; }
		public string NameCategory { get; set; }
		public System.DateTime ClaimDate { get; set; }
		public string IssueDescription { get; set; }
		public string Status { get; set; }
		public Nullable<System.DateTime> ResolvedDate { get; set; }

	}
}
