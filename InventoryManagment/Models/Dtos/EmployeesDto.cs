﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Models.Dtos
{
	internal class EmployeesDto
	{
		public int IdEmployee { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string Position { get; set; }
		public string Name { get; set; }
		public string HeadOfDepartment { get; set; }

		public string Location { get; set; }
	}
}