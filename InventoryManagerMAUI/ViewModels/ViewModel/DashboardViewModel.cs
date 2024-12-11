using Microsoft.EntityFrameworkCore;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagerMAUI.ViewModels.ViewModel
{
    public class DashboardViewModel : ViewModelBase<Equipment>
    {
        public DashboardViewModel()
        {
            GetEquipmentCostByCategory();
        }

        private ISeries[] _stockDistributionSeries;
        public int TotalEquipment { get; set; }
        public List<Equipment> ChartData { get; set; }

        public ISeries[] StockDistributionSeries
        {
            get => _stockDistributionSeries;
            set
            {
                if (Equals(value, _stockDistributionSeries)) return;
                _stockDistributionSeries = value;
                OnPropertyChanged(nameof(StockDistributionSeries));
            }
        }

        public void GetEquipmentCostByCategory()
        {
            using var db = new InventoryManagmentEntities();

            FormattableString query = $@"
        SELECT c.Name AS CategoryName, 
               SUM(e.Cost) AS TotalCost
        FROM Equipments e
        JOIN Category c ON e.CategoryID = c.CategoryID
        GROUP BY c.Name";
            // Execute the query using raw SQL and map it to CategoryCostSummary
            var groupedData = db.Database.SqlQuery<CategoryCostSummary>(query).ToList();

            // Prepare the data for the chart
            ChartData = groupedData.Select(g => new Equipment
            {
                Category = new Category { Name = g.CategoryName },
                Cost = g.TotalCost // TotalCost is already nullable decimal
            }).ToList();

            // Create the series for the chart
            StockDistributionSeries = groupedData.Select(g => new PieSeries<double>
            {
                Values = new double[] { (double)(g.TotalCost ?? 0m) }, // Cast TotalCost to double
                Name = g.CategoryName // Use dynamic category name
            }).ToArray();

            // Optionally, update the total equipment count if needed
            TotalEquipment = db.Equipments.Count();
        }



        // Data for line chart (as an example)
        public ISeries[] InventoryTrendSeries { get; set; } =
        {
            new LineSeries<double>
            {
                Values = new double[] { 31000, 40000, 35000, 50000, 49000, 60000, 70000, 85000, 84000, 85420 },
                Fill = null // Remove the fill
            }
        };

        public string[] InventoryTrendLabels { get; set; } = 
        { "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
    }

    // This class represents the result of the raw SQL query
    public class CategoryCostSummary
    {
        public string CategoryName { get; set; }
        public decimal? TotalCost { get; set; }
    }

}
