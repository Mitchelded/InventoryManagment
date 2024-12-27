using CommunityToolkit.Maui.Views;

using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.DetailViews;

public partial class OrderDetailsPopup : Popup
{
	private OrderDetail? _viewModel;
	public OrderDetailsPopup(OrderDetail? viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
	}

	private void CloseButton_OnClicked(object? sender, EventArgs e)
	{
		Close();
	}
}