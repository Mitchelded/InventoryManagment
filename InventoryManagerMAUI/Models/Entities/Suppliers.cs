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
    
    public partial class Suppliers
    {
        [Key]
        public int IdSuppliers { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
    }
}