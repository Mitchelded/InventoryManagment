namespace ARM_Vyz.Model.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public partial class Roles
	{
		public Roles()
		{
			People = new HashSet<People>();
		}

		[Key]
		public int RoleID { get; set; }


		public string RoleName { get; set; }
		public virtual ICollection<People> People { get; set; }
	}
}
