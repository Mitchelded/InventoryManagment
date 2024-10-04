using System;

namespace InventoryManagment.Models.Dtos
{
	internal class OrdersDto
	{
		public int IdOrder { get; set; }
		public DateTime OrderDate { get; set; }
		public string NameSupplier { get; set; }
		public string ContactInfo { get; set; }
		public string Status { get; set; }
		public decimal? TotalCost { get; set; }
		public string OrderItems { get; set; }

	}
}
