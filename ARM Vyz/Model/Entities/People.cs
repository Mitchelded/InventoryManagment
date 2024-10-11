namespace ARM_Vyz.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class People
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public People()
        {
            Faculties = new HashSet<Faculties>();
            ScientificDegrees = new HashSet<ScientificDegrees>();
        }

		public void AddDessertation(ScientificDegrees degrees)
		{

			ScientificDegrees.Add(degrees);

		}

		public int PeopleID { get; set; }

        public int? RoleID { get; set; }

        [Required]
        [StringLength(10)]
        public string FIO { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        public bool HaveAChild { get; set; }

        [Column(TypeName = "money")]
        public decimal? Scholarship { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Column(TypeName = "money")]
        public decimal? Salary { get; set; }

        public int DepartmentID { get; set; }

        public virtual Departments Departments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Faculties> Faculties { get; set; }

        public virtual Roles Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScientificDegrees> ScientificDegrees { get; set; }
    }
}
