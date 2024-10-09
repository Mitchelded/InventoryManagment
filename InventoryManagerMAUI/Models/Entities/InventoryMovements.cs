using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagment.Models;

namespace InventoryManagement.Models
{
    public partial class InventoryMovements
    {
        [Key] public int IdMovement { get; set; }

        [ForeignKey(nameof(Equipment))] public int EquipmentID { get; set; }

        [ForeignKey(nameof(FromLocation))] public int FromLocationID { get; set; }

        [ForeignKey(nameof(ToLocation))] public int ToLocationID { get; set; }

        public DateTime MovementDate { get; set; }

        [ForeignKey(nameof(MovedByEmployee))] public int MovedByEmployeeID { get; set; }

        [ForeignKey(nameof(ReceivedByEmployee))]
        public int? ReceivedByEmployeeID { get; set; }

        public Equipments Equipment { get; set; }
        public Locations FromLocation { get; set; }
        public Locations ToLocation { get; set; }
        public Employees MovedByEmployee { get; set; }
        public Employees ReceivedByEmployee { get; set; }
    }
}