using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InventoryMovements
    {
        [Key]
        public int IdMovement { get; set; }
        [ForeignKey("Equipments")]
        public int EquipmentID { get; set; }
        [ForeignKey("Locations")]
        public int FromLocationID { get; set; }
        [ForeignKey("Locations1")]
        public int ToLocationID { get; set; }
        public System.DateTime MovementDate { get; set; }
        [ForeignKey("Employees")]
        public int MovedByEmployeeID { get; set; }
        [ForeignKey("Employees1")]
        public Nullable<int> ReceivedByEmployeeID { get; set; }
    
        public Employees Employees { get; set; }
        public Employees Employees1 { get; set; }
        public Equipments Equipments { get; set; }
        public Locations Locations { get; set; }
        public Locations Locations1 { get; set; }
    }
}
