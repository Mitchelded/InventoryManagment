using InventoryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagment.Commands
{
	internal class InventoryMovementsMethods
	{
		static public void AddMovement(InventoryMovements movement)
		{
			try
			{
				try
				{
					using (InventoryManagmentEntities db = new InventoryManagmentEntities())
					{

						if (movement != null)
						{
							db.InventoryMovements.Add(movement);
							db.SaveChanges();
						}
						else
						{
							throw new Exception("Movement is null");
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


		static public void AddMovements(List<InventoryMovements> movement)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				if (movement != null && movement.Count > 0)
				{
					db.InventoryMovements.AddRange(movement);
					db.SaveChanges();
				}

			}
		}


		static public void DeleteMovement(InventoryMovements movement)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				db.InventoryMovements.Remove(movement);
				db.SaveChanges();
			}
		}

		static public void DeleteMovements(List<InventoryMovements> movement)
		{
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				db.InventoryMovements.RemoveRange(movement);
				db.SaveChanges();
			}
		}


		public static IEnumerable<InventoryMovementsDto> GetAllMovements()
		{
			using (var db = new InventoryManagmentEntities())
			{
				var Movements = (from move in db.InventoryMovements
								  join e in db.Equipments on move.IdMovement equals e.IdEquipment
								  join tL in db.Locations on move.ToLocationID equals tL.IdLocations
								  join fL in db.Locations on move.FromLocationID equals fL.IdLocations
								  join mE in db.Employees on move.MovedByEmployeeID equals mE.IdEmployee
								  join rE in db.Employees on move.ReceivedByEmployeeID equals rE.IdEmployee
								  select new InventoryMovementsDto
								  {
									  IdMovement = move.IdMovement,
									  Name = e.Name,
									  SerialNumber = e.Serial_Number,
									  PurchaseDate = e.PurchaseDate,
									  WarrantyExpiration = e.WarrantyExpiration,
									  Cost = e.Cost,

									  FromLocation = fL.Description,
									  ToLocation = fL.Description,
									  MovementDate = move.MovementDate,

									  MovedBy = mE.FirstName,
									  ReceivedBy = rE.FirstName,

								  });

				return Movements;
			}
		}

		public static IEnumerable<InventoryMovementsDto> GetMovementsByLocation(Locations to = null, Locations from = null)
		{
			try
			{
				using (InventoryManagmentEntities db = new InventoryManagmentEntities())
				{
					if (to == null)
						return GetAllMovements().Where(x => x.ToLocation == to.Description);
					else if (from == null)
						return GetAllMovements().Where(x => x.ToLocation == to.Description);
					else
						return GetAllMovements().Where(x => x.ToLocation == to.Description && x.FromLocation == from.Description);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
				return null;
			}
		}

	}
}
