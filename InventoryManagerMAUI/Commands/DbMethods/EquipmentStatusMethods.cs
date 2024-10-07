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
		public void Add(EquipmentStatus obj)
		{
			throw new NotImplementedException();
		}

		public void AddMany(List<EquipmentStatus> listObj)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<EquipmentStatus> GetAll()
		{
			throw new NotImplementedException();
		}

		public async void Remove(EquipmentStatus obj)
		{
			using InventoryManagmentEntities db = new InventoryManagmentEntities();
			db.EquipmentStatus.Remove(obj);
			await db.SaveChangesAsync();
		}

		public void RemoveMany(List<EquipmentStatus> listObj)
		{
			throw new NotImplementedException();
		}
	}
}
