//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace InventoryManagment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EquipmentStatus
    {
        [Key]
        public int IdStatus { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}