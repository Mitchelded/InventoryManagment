using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using InventoryManagement.Models;
using InventoryManagment.Interface;
using InventoryManagment.Models;
using InventoryManagment.Models.Dtos;

namespace InventoryManagment.Commands.DbMethods
{
    internal class InventoryMovementsMethods : IDbMethods<InventoryMovements, InventoryMovementsDto>
    {

        public IEnumerable<InventoryMovementsDto> GetMovementsByLocation(Locations to = null,Locations from = null)
        {
            try
            {
                if (to != null && from == null)
                    return GetAll().Where(x => x.ToLocation == to.Description);
                else if (from != null && to == null )
                    return GetAll().Where(x => x.ToLocation == from.Description);
                else
                    return GetAll().Where(x =>
                        x.ToLocation == to!.Description && x.FromLocation == from!.Description);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "Error");
                return null;
            }
        }

        public void Add(InventoryMovements obj)
        {
            try
            {
                try
                {
                    using InventoryManagmentEntities db = new InventoryManagmentEntities();
                    if (obj != null)
                    {
                        db.InventoryMovements.Add(obj);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Movement is null");
                    }
                }
                catch (Exception e)
                {
                    // MessageBox.Show(e.Message, "Error");
                    throw;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "Error");
            }
        }

        public void AddMany(List<InventoryMovements> listObj)
        {
            using InventoryManagmentEntities db = new InventoryManagmentEntities();
            if (listObj is { Count: > 0 })
            {
                db.InventoryMovements.AddRange(listObj);
                db.SaveChanges();
            }
        }

        public void Remove(InventoryMovements obj)
        {
            using InventoryManagmentEntities db = new InventoryManagmentEntities();
            db.InventoryMovements.Remove(obj);
            db.SaveChanges();
        }

        public IEnumerable<InventoryMovementsDto> GetAll()
        {
            using var db = new InventoryManagmentEntities();
            var movements = (from move in db.InventoryMovements
                join e in db.Equipments on move.IdMovement equals e.IdEquipment
                join tL in db.Locations on move.ToLocationID equals tL.IdLocations
                join fL in db.Locations on move.FromLocationID equals fL.IdLocations
                join mE in db.Employees on move.MovedByEmployeeID equals mE.IdEmployee
                join rE in db.Employees on move.ReceivedByEmployeeID equals rE.IdEmployee
                select new InventoryMovementsDto
                {
                    IdMovement = move.IdMovement,
                    NameEquipment = e.Name,
                    SerialNumber = e.Serial_Number,
                    PurchaseDate = e.PurchaseDate,
                    WarrantyExpiration = e.WarrantyExpiration,
                    Cost = e.Cost,

                    FromLocation = fL.Description,
                    ToLocation = tL.Description,
                    MovementDate = move.MovementDate,

                    MovedBy = mE.FirstName,
                    ReceivedBy = rE.FirstName,
                });

            return movements;
        }

        public void RemoveMany(List<InventoryMovements> listObj)
        {
            using InventoryManagmentEntities db = new InventoryManagmentEntities();
            db.InventoryMovements.RemoveRange(listObj);
            db.SaveChanges();
        }
    }
}