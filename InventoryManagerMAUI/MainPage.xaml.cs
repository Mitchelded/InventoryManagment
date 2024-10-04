using InventoryManagerMAUI.ViewModels;
using InventoryManagment.Models;

namespace InventoryManagerMAUI
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			BindingContext = new EquipmentStatusViewModel();
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				
			}
		}
	}
}
