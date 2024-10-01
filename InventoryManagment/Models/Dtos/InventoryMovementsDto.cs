using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Models.Dtos
{
	public class InventoryMovementsDto
	{
		public int IdMovement { get; set; }
		public string Name { get; set; }
		public string SerialNumber { get; set; }
		public string FromLocation { get; set; }
		public string ToLocation { get; set; }
		public System.DateTime MovementDate { get; set; }
		public System.DateTime PurchaseDate { get; set; }
		public System.DateTime? WarrantyExpiration { get; set; }

		public decimal? Cost { get; set; }
		public string MovedBy { get; set; }
		public string ReceivedBy { get; set; }

	}
}
