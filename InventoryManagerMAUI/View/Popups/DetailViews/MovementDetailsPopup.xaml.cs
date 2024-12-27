using CommunityToolkit.Maui.Views;

using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.DetailViews;

public partial class MovementDetailsPopup : Popup
{
	private EquipmentMovement? _viewModel;
	public MovementDetailsPopup(EquipmentMovement? viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
	}

	private void CloseButton_OnClicked(object? sender, EventArgs e)
	{
		Close();
	}
}