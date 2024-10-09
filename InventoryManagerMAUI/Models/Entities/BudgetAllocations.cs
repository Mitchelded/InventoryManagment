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
    
    public partial class BudgetAllocations
    {
        [Key]
        public int IDBudget { get; set; }
        public DateTime AllocationDate { get; set; }
        [ForeignKey(nameof(Departments))]
        public int DepartmentID { get; set; }
        public double Amount { get; set; }
        public string Purpose { get; set; }
    
        public virtual Departments Departments { get; set; }
    }
}
