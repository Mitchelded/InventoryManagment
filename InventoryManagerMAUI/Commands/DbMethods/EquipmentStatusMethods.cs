using InventoryManagment.Interface;
using InventoryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerMAUI.Commands.DbMethods
{
	internal class EquipmentStatusMethods : IDbMethods<EquipmentStatus, EquipmentStatus>
	{
		public async void Add(EquipmentStatus obj)
		{
			using InventoryManagmentEntities db = new InventoryManagmentEntities();
			await db.EquipmentStatus.AddAsync(obj);
			await db.SaveChangesAsync();
		}

		public async void AddMany(List<EquipmentStatus> listObj)
		{
			using InventoryManagmentEntities db = new InventoryManagmentEntities();
			await db.EquipmentStatus.AddRangeAsync(listObj);
			await db.SaveChangesAsync();
		}

		public IEnumerable<EquipmentStatus> GetAll()
		{
			using InventoryManagmentEntities db = new InventoryManagmentEntities();
			return db.EquipmentStatus;
		}

		public async void Remove(EquipmentStatus obj)
		{
			using InventoryManagmentEntities db = new InventoryManagmentEntities();
			db.EquipmentStatus.Remove(obj);
			await db.SaveChangesAsync();
		}

		public async void RemoveMany(List<EquipmentStatus> listObj)
		{
			using InventoryManagmentEntities db = new InventoryManagmentEntities();
			db.EquipmentStatus.RemoveRange(listObj);
			await db.SaveChangesAsync();
		}
	}
}
