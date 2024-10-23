namespace ARM_Vyz.Model.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	using System.Linq;

	public partial class People
	{
		public People()
		{
			Dessertations = new HashSet<Dessertations>();
			Faculties = new HashSet<Faculties>();
		}

		public void AddDessertation(Dessertations degrees)
		{

			Dessertations.Add(degrees);

		}

		public void AddDean(Faculties faculties)
		{
			if(faculties.DeanID == null || faculties.DeanID ==0)
			{
				faculties.DeanID = PeopleID;
				Faculties.Add(faculties);
			}

		}

		public int PeopleID { get; set; }

		public int? RoleID { get; set; }

		[Required]

		public string FIO { get; set; }

		[Column(TypeName = "date")]
		public DateTime Birthday { get; set; }
		[Required]
		public bool HaveAChild { get; set; }
		
		public bool Approved { get; set; }

		public int? NumberOfChildren { get; set; }

		[Column(TypeName = "money")]
		public decimal? Scholarship { get; set; }

		public string ScholarshipPurpose { get; set; }

		[Required]

		public string Gender { get; set; }


		[Required]

		public string Login { get; set; }

		[Required]
		public string Password { get; set; }
		public string Course { get; set; }

		[Column(TypeName = "money")]
		public decimal? Salary { get; set; }

		public int? DepartmentID { get; set; }

		public Departments Departments { get; set; }
		public ICollection<Dessertations> Dessertations { get; set; }

		public ICollection<Faculties> Faculties { get; set; }

		public Roles Roles { get; set; }
	}
}
