namespace ARM_Vyz.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScientificDegrees
    {
        [Key]
        public int ScientificDegreeID { get; set; }

        [StringLength(150)]
        public string NameDissertation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Year { get; set; }

        public int? TeacherID { get; set; }

        public virtual Peoples Peoples { get; set; }
    }
}
