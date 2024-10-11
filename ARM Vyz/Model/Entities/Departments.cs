namespace ARM_Vyz.Model.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public partial class Departments
	{
		public Departments()
		{
			People = new HashSet<People>();
		}

		[Key]
		public int DepartmentID { get; set; }

		[Required]
		public string DepartmentName { get; set; }

		public int? FacultyID { get; set; }

		public virtual Faculties Faculties { get; set; }
		public virtual ICollection<People> People { get; set; }
	}
}
