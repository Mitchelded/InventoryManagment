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
    
    public partial class Audits
    {
        [Key]
        public int IdAudit { get; set; }
        public System.DateTime AuditDate { get; set; }
        [ForeignKey(nameof(Employees))]
        public int PerformedByEmployeeID { get; set; }
        public string Notes { get; set; }
        public string Discrepancies { get; set; }
    
        public virtual Employees Employees { get; set; }
    }
}
