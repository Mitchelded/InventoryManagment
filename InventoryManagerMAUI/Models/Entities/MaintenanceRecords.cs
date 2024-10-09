//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaintenanceRecords
    {
        [Key]
        public int IdMaintenance { get; set; }
        
        [ForeignKey(nameof(Equipments))]
        public int EquipmentID { get; set; }
        
        public System.DateTime MaintensnceDate { get; set; }
        [ForeignKey(nameof(Employees))]
        public Nullable<int> PerformedByEmployeeID { get; set; }
        
        public string MaintenanceType { get; set; }
        public Nullable<decimal> Cost { get; set; }
    
        public Employees Employees { get; set; }
        public Equipments Equipments { get; set; }
    }
}
