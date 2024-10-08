using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels;
using InventoryManagment.Models;

namespace InventoryManagerMAUI
{
	public partial class MainPage : ContentPage
	{
		public EquipmentStatusViewModel viewModel = new EquipmentStatusViewModel();
		public MainPage()
		{
			InitializeComponent();
			BindingContext = viewModel;

			using (InventoryManagmentEntities db = new InventoryManagmentEntities())
			{

			}
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			var popup = new BaseContentPageClass<EquipmentStatusViewModel>(
	new Dictionary<string, string>
	{
					{ "Enter status Name", "Name"},
					{"Enter status Description", "Description"},
	}, this.viewModel);
			this.ShowPopupAsync(popup);
		}
	}
}
