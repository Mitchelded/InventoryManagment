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

		private void btn_Add_Clicked(object sender, EventArgs e)
		{
			Dictionary<string, string> labelsDict = new Dictionary<string, string>
			{
					{ "Enter status Name", "Name"},
					{"Enter status Description", "Description"},
			};
			var popup = new InputPopup<EquipmentStatusViewModel>(labelsDict, this.viewModel);
			this.ShowPopupAsync(popup);
		}
	}
}
