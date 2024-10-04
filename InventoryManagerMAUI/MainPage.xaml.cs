using InventoryManagerMAUI.ViewModels;
using InventoryManagment.Models;

namespace InventoryManagerMAUI
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			BindingContext = new EquipmentsViewModel();
			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{
				
			}
		}
	}
}
