using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.Interface;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace InventoryManagerMAUI.ViewModels.Popups.AddViews;

public partial class AddOrderPopup : Popup
{
    private readonly OrderDetail? _viewModel;

    public AddOrderPopup(OrderDetail? viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
    }


    private void Button_Clicked(object sender, EventArgs e)
    {
        
    }
}
