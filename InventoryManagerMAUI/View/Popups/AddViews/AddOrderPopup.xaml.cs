using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.ViewModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace InventoryManagerMAUI.ViewModels.Popups.AddViews;

public partial class AddOrderPopup : Popup
{
    private readonly OrderDetail? _viewModel;

    public AddOrderPopup(OrdersManagementViewModel? viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }

    private void CancelBtn_OnClicked(object? sender, EventArgs e)
    {
        Close();
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        
    }
}
