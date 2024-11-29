using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.View;

public partial class CategoryView : ContentPage
{
	private readonly InventoryManagmentEntities _db = new();

	private CategoryViewModel viewModel;
	public CategoryView()
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
					{ "Enter Name", "Name"},
					{"Enter Description", "Description"},
			};
		var popup = new InputPopup<CategoryViewModel>(labelsDict, viewModel);
		this.ShowPopupAsync(popup);
	}

	private void Button_OnClicked(object? sender, EventArgs e)
	{
		//Navigation.PushAsync(new BudgetAllocations());
	}
}