using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels;
using InventoryManagerMAUI.ViewModels.ViewModel;
using InventoryManagment.Models;

namespace InventoryManagerMAUI.View;

public partial class EquipmentStatusView : ContentPage
{
	private readonly InventoryManagmentEntities _db = new();

	private EquipmentStatusViewModel viewModel;
	public EquipmentStatusView()
	{
		InitializeComponent();
		viewModel = new();
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
		var popup = new InputPopup<EquipmentStatusViewModel>(labelsDict, viewModel);
		this.ShowPopupAsync(popup);
	}

	private void Button_OnClicked(object? sender, EventArgs e)
	{
		Navigation.PushAsync(new BudgetAllocations());
	}
}