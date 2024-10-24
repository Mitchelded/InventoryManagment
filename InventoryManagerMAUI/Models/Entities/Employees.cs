using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagment.Models;

namespace InventoryManagement.Models
{
    public partial class Employees
    {
        [Key] public int IdEmployee { get; set; }

        [MaxLength(50)] public string FirstName { get; set; }

        [MaxLength(50)] public string LastName { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        [MaxLength(50)] public string Patronymic { get; set; } // Optional: consider making this nullable

        [MaxLength(50)] public string Position { get; set; }

        [ForeignKey(nameof(Department))] public int DepartmentID { get; set; }

        public virtual Departments Department { get; set; }
        public virtual ICollection<Audits> Audits { get; set; } = new List<Audits>();
        public virtual ICollection<InventoryMovements> MovedInventory { get; set; } = new List<InventoryMovements>();

        public virtual ICollection<InventoryMovements> ReceivedInventoryMovements { get; set; } =
            new List<InventoryMovements>();

        public virtual ICollection<MaintenanceRecords> MaintenanceRecords { get; set; } =
            new List<MaintenanceRecords>();

        public virtual ICollection<UtilizationRecords> UtilizationRecords { get; set; } =
            new List<UtilizationRecords>();
    }
}