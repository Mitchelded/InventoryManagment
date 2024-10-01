using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Models.Dtos
{
	internal class LocationsDto
	{
		public int IdLocations { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public string HeadOfDepartment { get; set; }
	}
}
