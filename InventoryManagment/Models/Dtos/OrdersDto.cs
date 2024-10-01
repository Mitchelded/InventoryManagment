using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Models.Dtos
{
	internal class OrdersDto
	{
		public int IdOrder { get; set; }
		public System.DateTime OrderDate { get; set; }
		public string Name { get; set; }
		public string ContactInfo { get; set; }
		public string Status { get; set; }
		public Nullable<decimal> TotalCost { get; set; }
		public string OrderItems { get; set; }

	}
}
