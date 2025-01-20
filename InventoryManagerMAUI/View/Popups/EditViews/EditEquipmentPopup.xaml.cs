using CommunityToolkit.Maui.Views;

using System.Runtime.CompilerServices;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditEquipmentPopup : Popup
{
	private readonly EquipmentsViewModel _viewModel;
	private Equipment _editEquipment;
	public EditEquipmentPopup(EquipmentsViewModel viewModel, Equipment editEquipment = null)
	{
		_viewModel = viewModel;
		_editEquipment = editEquipment;
		InitializeComponent();
		BindingContext = _viewModel;
	}

	private void CancelBtn_OnClicked(object? sender, EventArgs e)
	{
		Close();
	}
}