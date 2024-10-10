using System.Collections.ObjectModel;
using InventoryManagment.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel
{
    class BudgetAllocationsViewModel : ViewModelBase<BudgetAllocations>
    {
	    public BudgetAllocationsViewModel() : base()
		{

	    }

	    public PieSeries<double>[] Series { get; set; }

	    private void GenerateSeries()
	    {
		    // Group the budget data by department
		    var groupedData = Collection
			    .GroupBy(data => data.DepartmentID)
			    .Select(g => new
			    {
				    DepartmentId = g.Key,
				    TotalAmount = g.Sum(data => data.Amount)
			    })
			    .ToList();

		    // Create the pie series based on the grouped data
		    Series = groupedData.Select(g => new PieSeries<double>
		    {
			    Values = new Double[] { g.TotalAmount }
		    }).ToArray();
	    }

		public override void OnAdd(object obj)
		{
			base.OnAdd(obj);
		}

		private int _idBudget;
	    private string _allocationDate;
	    private int _departmentId;
	    private string _amount;
	    private string _purpose;

	    public int IDBudget
	    {
		    get => _idBudget;
		    set
		    {
			    if (value == _idBudget) return;
			    _idBudget = value;
			    OnPropertyChanged(nameof(IDBudget));
		    }
	    }

	    public string AllocationDate
	    {
		    get => _allocationDate;
		    set
		    {
			    if (value == _allocationDate) return;
			    _allocationDate = value;
			    OnPropertyChanged(nameof(AllocationDate));
		    }
	    }

	    public int DepartmentID
	    {
		    get => _departmentId;
		    set
		    {
			    if (value == _departmentId) return;
			    _departmentId = value;
			    OnPropertyChanged(nameof(DepartmentID));
		    }
	    }

	    public string Amount
	    {
		    get => _amount;
		    set
		    {
			    if (value == _amount) return;
			    _amount = value;
			    OnPropertyChanged(nameof(Amount));
		    }
	    }

	    public string Purpose
	    {
		    get => _purpose;
		    set
		    {
			    if (value == _purpose) return;
			    _purpose = value;
			    OnPropertyChanged(nameof(Purpose));
		    }
	    }
    }
}
