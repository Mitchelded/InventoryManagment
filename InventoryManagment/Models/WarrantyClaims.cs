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
    
    public partial class WarrantyClaims
    {
        public int IdWarranty { get; set; }
        public int EquipmentID { get; set; }
        public System.DateTime ClaimDate { get; set; }
        public string IssueDescription { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> ResolvedDate { get; set; }
    
        public virtual Equipments Equipments { get; set; }
    }
}
