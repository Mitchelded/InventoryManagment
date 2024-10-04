using System;

namespace InventoryManagment.Models.Dtos
{
	internal class UtilizationDto
	{
		public int IdUtilization { get; set; }
		public string NameEquipment { get; set; }
		public string SerialNumber { get; set; }
		public string NameCategory { get; set; }
		public string LastNameStudent { get; set; }
		public string LastNameEmployee { get; set; }
		public DateTime UsageStart { get; set; }
		public DateTime? UsageEnd { get; set; }
		public string Purpose { get; set; }
	}
}
