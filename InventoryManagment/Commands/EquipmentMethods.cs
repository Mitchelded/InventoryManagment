using InventoryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagment.Commands
{
	static class EquipmentMethods
	{

		static public void AddEquipment(Equipments equipment)
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


		static public void AddEquipments(List<Equipments> equipments)
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


		static public void DeleteEquipment(Equipments equipment)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				db.Equipments.Remove(equipment);
				db.SaveChanges();
			}
		}

		static public void DeleteEquipments(List<Equipments> equipments)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				db.Equipments.RemoveRange(equipments);
				db.SaveChanges();
			}
		}


		static public IEnumerable<Equipments> GetAllEquipmetsFromLocation(Locations locations)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				IEnumerable<Equipments> equipments = db.Equipments.Where(x => x.LocationID == locations.IdLocations);
				if (equipments != null && equipments.Count() > 0)
					return equipments;
				else
					return null;
			}

		}

		public static IEnumerable<EquipmentDto> GetAllEquipments()
		{
			using (var db = new InventoryManagmentEntities())
			{
				#region first_variant
				var equipments = (from equipment in db.Equipments
								  join supplier in db.Suppliers on equipment.SupplierID equals supplier.IdSuppliers
								  join department in db.Departments on equipment.DepartmentID equals department.IdDepartments
								  join category in db.Categories on equipment.CategoryID equals category.IdCategories
								  join location in db.Locations on equipment.LocationID equals location.IdLocations
								  join status in db.EquipmentStatus on equipment.StatusID equals status.IdStatus
								  select new EquipmentDto
								  {
									  Name = equipment.Name,
									  SerialNumber = equipment.Serial_Number,
									  Category = category.Name,
									  Department = department.Name,
									  Location = location.Description,
									  Status = status.Name,
									  PurchaseDate = equipment.PurchaseDate,
									  WarrantyExpiration = equipment.WarrantyExpiration,
									  Supplier = supplier.Name,
									  Cost = equipment.Cost
								  });
				#endregion

				#region second_variant:
				//var equipments2 = db.Equipments
				//.Join(db.Suppliers,
				//	  equipment => equipment.SupplierID,
				//	  supplier => supplier.IdSuppliers,
				//	  (equipment, supplier) => new { equipment, supplier })
				//.Join(db.Departments,
				//	  es => es.equipment.DepartmentID,
				//	  department => department.IdDepartments,
				//	  (es, department) => new
				//	  {
				//		  es.equipment,
				//		  es.supplier,
				//		  department
				//	  })
				//.Select(e => new
				//{
				//	e.equipment.IdEquipment,
				//	e.equipment.Name,
				//	SupplierName = e.supplier.Name,
				//	DepartmentName = e.department.Name
				//});
				#endregion


				return equipments;
			}
		}

	}
}
