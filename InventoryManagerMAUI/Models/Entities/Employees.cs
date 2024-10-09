using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employees
    {
        [Key]
        public int IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
        [ForeignKey("Departments")]
        public int DepartmentID { get; set; }
        

        public Departments Departments { get; set; }
        public ICollection<Audits> Audits { get; set; }
        public ICollection<InventoryMovements> InventoryMovements { get; set; }
        public ICollection<InventoryMovements> InventoryMovements1 { get; set; }
        public ICollection<MaintenanceRecords> MaintenanceRecords { get; set; }
        public ICollection<UtilizationRecords> UtilizationRecords { get; set; }
    }
}
