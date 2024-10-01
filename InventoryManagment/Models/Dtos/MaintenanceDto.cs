using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Models.Dtos
{
	internal class MaintenanceDto
	{
		public int IdMaintenance { get; set; }
		public string NameEquipment { get; set; }
		public string Serial_Number { get; set; }
		public string NameCategory { get; set; }
		public System.DateTime MaintensnceDate { get; set; }
		public string FirstName { get; set; }
		public string MaintenanceType { get; set; }
		public Nullable<decimal> Cost { get; set; }
	}
}
