using System.Collections.Generic;
using InventoryManagment.Models;

namespace InventoryManagment.Interface
{
    public interface IDbMethods<T, TD>
    {
        void Add(T obj);
        void AddMany(List<T> listObj);
        void Remove(T obj);
        IEnumerable<TD> GetAll();
        void RemoveMany(List<T> listObj);
        
    }
}