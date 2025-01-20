using CommunityToolkit.Maui.Views;

using System.Runtime.CompilerServices;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditWarrantyClaimPopup : Popup
{
	private readonly WarrantyClaim? _viewModel;
	private WarrantyClaim _editEquipment;
	public EditWarrantyClaimPopup(WarrantyClaimsViewModel? viewModel, WarrantyClaim editEquipment = null)
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