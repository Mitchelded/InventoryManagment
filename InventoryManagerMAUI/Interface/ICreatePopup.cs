using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerMAUI.Interface
{
	internal interface ICreatePopup<T>
	{
		void CreatePopup(T viewModel);
	}
}
