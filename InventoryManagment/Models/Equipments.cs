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
	using System.Linq;
	using System.Windows;

	public partial class Equipments
	{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equipments()
        {
            this.InventoryMovements = new HashSet<InventoryMovements>();
            this.MaintenanceRecords = new HashSet<MaintenanceRecords>();
            this.MaintenanceRecords1 = new HashSet<MaintenanceRecords>();
            this.UtilizationRecords = new HashSet<UtilizationRecords>();
            this.WarrantyClaims = new HashSet<WarrantyClaims>();
        }

		public void AddEquipment(Equipments equipment)
		{
			try
			{
				try
				{
					using (InventoryManagmentEntities db = new InventoryManagmentEntities())
					{

						if (equipment != null)
						{
							db.Equipments.Add(equipment);
							db.SaveChanges();
						}
						else
						{
							throw new Exception("Equipment is null");
						}

					}
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Error");
					throw;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
			}
		}


		public void AddEquipments(List<Equipments> equipments)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				if (equipments != null && equipments.Count > 0)
				{
					db.Equipments.AddRange(equipments);
					db.SaveChanges();
				}

			}
		}


		public void DeleteEquipment(Equipments equipment)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				db.Equipments.Remove(equipment);
				db.SaveChanges();
			}
		}

		public void DeleteEquipments(List<Equipments> equipments)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				db.Equipments.RemoveRange(equipments);
				db.SaveChanges();
			}
		}


		public IEnumerable<Equipments> GetAllEquipmetsFromLocation(Locations locations)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				IEnumerable<Equipments> equipments = db.Equipments.Where(x => x.LocationID == locations.IdLocations);
				if (equipments !=null && equipments.Count() > 0)
					return equipments;
				else
					return null;
			}
			
		}



		public int IdEquipment { get; set; }
        public string Name { get; set; }
        public string Serial_Number { get; set; }
        public int CategoryID { get; set; }
        public int DepartmentID { get; set; }
        public int LocationID { get; set; }
        public int StatusID { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public Nullable<System.DateTime> WarrantyExpiration { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<decimal> Cost { get; set; }
    
        public virtual Categories Categories { get; set; }
        public virtual Departments Departments { get; set; }
        public virtual EquipmentStatus EquipmentStatus { get; set; }
        public virtual Locations Locations { get; set; }
        public virtual Suppliers Suppliers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryMovements> InventoryMovements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaintenanceRecords> MaintenanceRecords { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaintenanceRecords> MaintenanceRecords1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UtilizationRecords> UtilizationRecords { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarrantyClaims> WarrantyClaims { get; set; }
    }
}
