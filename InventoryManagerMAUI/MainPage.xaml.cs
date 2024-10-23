using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.View;
using InventoryManagerMAUI.ViewModels;
using InventoryManagerMAUI.ViewModels.ViewModel;
using InventoryManagment.Models;
using BudgetAllocations = InventoryManagerMAUI.View.BudgetAllocations;

namespace InventoryManagerMAUI
{
	public partial class MainPage : ContentPage
	{

		public MainPage()
		{
			InitializeComponent();

		}

		private void EquipmentStatus_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new EquipmentStatusView());
		}

		private void AuditsView_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AuditsView());
		}

		private void CategoriesView_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new CategoriesView());
		}

		private void EquipmentView_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new EquipmentsView());
		}
	}
}
