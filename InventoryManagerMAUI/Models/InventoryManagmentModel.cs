﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace InventoryManagment.Models
{
    using System;
    using System.Linq;
    
    public partial class InventoryManagmentEntities : DbContext
    {
        public InventoryManagmentEntities()
        {
            Database.EnsureCreated();
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=InventoryManagment.db");
        }

        public virtual DbSet<Audits> Audits { get; set; }
        public virtual DbSet<BudgetAllocations> BudgetAllocations { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Equipments> Equipments { get; set; }
        public virtual DbSet<EquipmentStatus> EquipmentStatus { get; set; }
        public virtual DbSet<InventoryMovements> InventoryMovements { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<MaintenanceRecords> MaintenanceRecords { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<UtilizationRecords> UtilizationRecords { get; set; }
        public virtual DbSet<WarrantyClaims> WarrantyClaims { get; set; }

    }
}