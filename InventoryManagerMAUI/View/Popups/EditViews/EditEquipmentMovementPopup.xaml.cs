using CommunityToolkit.Maui.Views;

using System.Runtime.CompilerServices;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditEquipmentMovementPopup : Popup
{
	private readonly EquipmentMovement? _viewModel;
	private EquipmentMovement _editEquipment;
	public EditEquipmentMovementPopup(EquipmentMovementViewModel? viewModel, EquipmentMovement editEquipment = null)
	{

		_editEquipment = editEquipment;
		InitializeComponent();
		BindingContext = _viewModel;
	}

	private void CancelBtn_OnClicked(object? sender, EventArgs e)
	{
		Close();
	}
}