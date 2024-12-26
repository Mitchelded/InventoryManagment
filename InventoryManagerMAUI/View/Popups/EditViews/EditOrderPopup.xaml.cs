using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.Interface;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditOrderPopup : Popup
{
    private readonly OrderDetail? _viewModel;

    public EditOrderPopup(OrderDetail? viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
    }


    private void Button_Clicked(object sender, EventArgs e)
    {
        
    }
}
