//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryManagment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaintenanceRecords
    {
        public int IdMaintenance { get; set; }
        public int EquipmentID { get; set; }
        public System.DateTime MaintensnceDate { get; set; }
        public Nullable<int> PerformedByEmployeeID { get; set; }
        public string MaintenanceType { get; set; }
        public Nullable<decimal> Cost { get; set; }
    
        public virtual Employees Employees { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Equipment Equipment1 { get; set; }
    }
}
