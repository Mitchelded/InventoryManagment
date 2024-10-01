using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Commands
{
	internal class GetInstancesView
	{
		public static T GetInstanceView<T>(object sender) where T : class
		{
			var packIcon = sender as PackIcon;
			if (packIcon != null)
			{
				var employeeEdit = packIcon.DataContext as T;
				return employeeEdit;
			}
			return default;
		}
	}
}
