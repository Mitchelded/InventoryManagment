namespace ARM_Vyz.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Faculties
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Faculties()
        {
            Departments = new HashSet<Departments>();
        }

        [Key]
        public int FacultyID { get; set; }

        [Required]
        [StringLength(10)]
        public string FacultyName { get; set; }

        public int? DeanID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Departments> Departments { get; set; }

        public virtual People People { get; set; }
    }
}
