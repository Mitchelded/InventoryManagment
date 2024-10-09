using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagerMAUI.ViewModels;

namespace InventoryManagerMAUI.View;

public partial class BudgetAllocations : ContentPage
{
    public BudgetAllocations()
    {
        InitializeComponent();
        BindingContext = new BudgetAllocationsViewModel();
    }
}