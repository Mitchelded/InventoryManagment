using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.Interface;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.DetailViews;

public partial class WarrantyClaimDetailsPopup : Popup
{
	private WarrantyClaim? _viewModel;
	public WarrantyClaimDetailsPopup(WarrantyClaim? viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
	}

	private void CloseButton_OnClicked(object? sender, EventArgs e)
	{
		Close();
	}
}