using InventoryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagment.Commands
{
	internal class SuppliersMethods
	{
		static public void AddEquipment(Suppliers suppliers)
		{
			try
			{
				try
				{
					using (InventoryManagmentEntities db = new InventoryManagmentEntities())
					{

						if (suppliers != null)
						{
							db.Suppliers.Add(suppliers);
							db.SaveChanges();
						}
						else
						{
							throw new Exception("Suppliers is null");
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

		static public void AddEquipments(List<Suppliers> suppliers)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				if (suppliers != null && suppliers.Count > 0)
				{
					db.Suppliers.AddRange(suppliers);
					db.SaveChanges();
				}

			}
		}

		static public void DeleteEquipment(Suppliers suppliers)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				db.Suppliers.Remove(suppliers);
				db.SaveChanges();
			}
		}

		static public void DeleteEquipments(List<Suppliers> suppliers)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				db.Suppliers.RemoveRange(suppliers);
				db.SaveChanges();
			}
		}

		public static IEnumerable<Suppliers> GetAllEquipments()
		{
			using (var db = new InventoryManagmentEntities())
			{
				return db.Suppliers;
			}
		}
	}
}
