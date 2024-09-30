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

		static public IEnumerable<Equipments> GetAllEquipmets()
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				return db.Equipments.Include("Suppliers").ToList();
			}
		}
	}
}
