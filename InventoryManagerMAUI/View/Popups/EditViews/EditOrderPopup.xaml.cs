using CommunityToolkit.Maui.Views;

using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditOrderPopup : Popup
{
    private readonly OrderDetail? _viewModel;

    public EditOrderPopup(OrderDetail? viewModel, OrdersManagementViewModel viewModel2 = null)
	{
		InitializeComponent();

        _viewModel = viewModel;
        viewModel2.LoadProduct();
    }
    private void CancelBtn_OnClicked(object? sender, EventArgs e)
    {
        Close();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        
    }
}
