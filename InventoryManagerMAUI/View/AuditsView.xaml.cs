using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels;
using InventoryManagerMAUI.ViewModels.Popups;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.View;

public partial class AuditsView : ContentPage
{
	private readonly InventoryManagmentEntities _db = new();

	private AuditsViewModel viewModel;
	public AuditsView()
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
					{ "Enter AuditDate", "AuditDate"},
					{"Enter PerformedByEmployeeID", "PerformedByEmployeeID"},
					{"Enter Notes", "Notes"},
					{"Enter Discrepancies", "Discrepancies"},
			};
		var popup = new InputPopup<AuditsViewModel>(labelsDict, viewModel);
		this.ShowPopupAsync(popup);
	}

	private void Button_OnClicked(object? sender, EventArgs e)
	{
		Navigation.PushAsync(new BudgetAllocations());
	}
}