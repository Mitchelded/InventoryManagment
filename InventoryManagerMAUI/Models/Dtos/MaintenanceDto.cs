using System;

namespace InventoryManagment.Models.Dtos
{
	internal class MaintenanceDto
	{
		public int IdMaintenance { get; set; }
		public string NameEquipment { get; set; }
		public string SerialNumber { get; set; }
		public string NameCategory { get; set; }
		public DateTime MaintensnceDate { get; set; }
		public string PerfomedBy { get; set; }
		public string MaintenanceType { get; set; }
		public decimal? Cost { get; set; }
	}
}
