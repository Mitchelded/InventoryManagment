using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Models.Dtos
{
	internal class AuditsDto
	{
		public int IdAudit { get; set; }
		public System.DateTime AuditDate { get; set; }
		public string PerfomedBy { get; set; }
		public string Notes { get; set; }
		public string Discrepancies { get; set; }
	}
}
