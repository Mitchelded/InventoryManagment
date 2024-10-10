using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagerMAUI.ViewModels.ViewModel;
using InventoryManagment.Models;

namespace InventoryManagerMAUI.View;

public partial class BudgetAllocations : ContentPage
{
	private readonly InventoryManagmentEntities _db = new();

	private BudgetAllocationsViewModel viewModel;

	public BudgetAllocations()
    {
        InitializeComponent();
		viewModel = new();
		BindingContext = viewModel;
    }
}