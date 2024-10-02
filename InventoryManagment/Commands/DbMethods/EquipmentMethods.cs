using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using InventoryManagment.Interface;
using InventoryManagment.Models;
using InventoryManagment.Models.Dtos;

namespace InventoryManagment.Commands.DbMethods
{
	internal class EquipmentMethods : IDbMethods<Equipments, EquipmentDto>
	{
		public IEnumerable<EquipmentDto> GetAllEquipmentsFromLocation(Locations locations)
		{
			using InventoryManagmentEntities db = new InventoryManagmentEntities();
			IEnumerable<EquipmentDto> equipments = GetAll().Where(x => x.Location == locations.Description);
			IEnumerable<EquipmentDto> allEquipmentsFromLocation = equipments.ToList();
			if (allEquipmentsFromLocation.Any())
				return allEquipmentsFromLocation;
			else
				return null;
		}

		public void Add(Equipments obj)
		{
			try
			{
				try
				{
					using InventoryManagmentEntities db = new InventoryManagmentEntities();
					if (obj != null)
					{
						db.Equipments.Add(obj);
						db.SaveChanges();
					}
					else
					{
						throw new Exception("Equipment is null");
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

		public void AddMany(List<Equipments> listObj)
		{
			using InventoryManagmentEntities db = new InventoryManagmentEntities();
			if (listObj is { Count: > 0 })
			{
				db.Equipments.AddRange(listObj);
				db.SaveChanges();
			}
		}

		public void Remove(Equipments obj)
		{
			using InventoryManagmentEntities db = new InventoryManagmentEntities();
			db.Equipments.Remove(obj);
			db.SaveChanges();
		}

		public IEnumerable<EquipmentDto> GetAll()
		{
			using var db = new InventoryManagmentEntities();

			#region first_variant
			var equipments = (from equipment in db.Equipments
				join supplier in db.Suppliers on equipment.SupplierID equals supplier.IdSuppliers
				join department in db.Departments on equipment.DepartmentID equals department.IdDepartments
				join category in db.Categories on equipment.CategoryID equals category.IdCategories
				join location in db.Locations on equipment.LocationID equals location.IdLocations
				join status in db.EquipmentStatus on equipment.StatusID equals status.IdStatus
				select new EquipmentDto
				{
					IdEquipment = equipment.IdEquipment,
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

		public void RemoveMany(List<Equipments> listObj)
		{
			using InventoryManagmentEntities db = new InventoryManagmentEntities();
			db.Equipments.RemoveRange(listObj);
			db.SaveChanges();
		}
	}
}
