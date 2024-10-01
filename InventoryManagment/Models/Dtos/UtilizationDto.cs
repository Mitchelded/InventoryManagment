using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Models.Dtos
{
	internal class UtilizationDto
	{
		public int IdUtilization { get; set; }
		public string NameEquipment { get; set; }
		public string Serial_Number { get; set; }
		public string NameCategory { get; set; }
		public string LastNameStudent { get; set; }
		public string LastNameEmployee { get; set; }
		public System.DateTime UsageStart { get; set; }
		public Nullable<System.DateTime> UsageEnd { get; set; }
		public string Purpose { get; set; }
	}
}
