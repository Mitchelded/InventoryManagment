using System;

namespace InventoryManagment.Models.Dtos
{
	public class EquipmentDto
	{
		public int IdEquipment { get; set; }
		public string Name { get; set; }
		public string SerialNumber { get; set; }
		public string Category { get; set; }
		public string Department { get; set; }
		public string Location { get; set; }
		public string Status { get; set; }
		public DateTime PurchaseDate { get; set; }
		public DateTime? WarrantyExpiration { get; set; }
		public string Supplier { get; set; }
		public decimal? Cost { get; set; }
	}
}
