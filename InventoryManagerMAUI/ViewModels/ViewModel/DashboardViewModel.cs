using Microsoft.EntityFrameworkCore;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using System.Linq;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace InventoryManagerMAUI.ViewModels.ViewModel
{
    public class DashboardViewModel : ViewModelBase<Equipment>
    {
        public DashboardViewModel()
        {
            GetEquipmentCostByCategory();
            GetTotalEquipment();
            GetAllCost();
            GetLowStock();
        }


        private ISeries[] _stockDistributionSeries;
        private List<Equipment> _chartData;
        private List<LowStockAlert> _lowStockEquipment;
        private int _totalEquipment { get; set; }
        private decimal? _totalCost { get; set; }

        public List<Equipment> ChartData
        {
            get => _chartData;
            set
            {
                if (Equals(value, _chartData)) return;
                _chartData = value;
                OnPropertyChanged(nameof(ChartData));
            }
        }

        public List<LowStockAlert> LowStockEquipment
        {
            get => _lowStockEquipment;
            set
            {
                if (Equals(value, _lowStockEquipment)) return;
                _lowStockEquipment = value;
                OnPropertyChanged(nameof(LowStockEquipment));
            }
        }


        public ISeries[] StockDistributionSeries
        {
            get => _stockDistributionSeries;
            set
            {
                if (Equals(value, _stockDistributionSeries)) return;
                _stockDistributionSeries = value;
                OnPropertyChanged(nameof(StockDistributionSeries));
                OnPropertyChanged(nameof(StockDistributionSeries));
            }
        }

        public int TotalEquipment
        {
            get => _totalEquipment;
            set
            {
                if (Equals(value, _totalEquipment)) return;
                _totalEquipment = value;
                OnPropertyChanged(nameof(TotalEquipment));
                OnPropertyChanged(nameof(TotalEquipment));
            }
        }

        public decimal? TotalCost
        {
            get => _totalCost;
            set
            {
                if (Equals(value, _totalCost)) return;
                _totalCost = value;
                OnPropertyChanged(nameof(TotalCost));
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        private void GetLowStock()
        {
            using var db = new InventoryManagmentEntities();
            try
            {
                FormattableString query = $@"
                        SELECT 
                e.Name AS EquipmentName,
                c.Quantity,
                CASE 
                    WHEN c.Quantity = 0 THEN 'Stock Out Alert'
                    WHEN c.Quantity < 10 THEN 'Low Stock Warning'
                    ELSE 'Restock Complete'
                END AS StockStatus
            FROM Equipments e
            JOIN Stocks c ON e.EquipmentID = c.EquipmentID
            WHERE c.Quantity < 20
            GROUP BY e.Name, c.Quantity;
            ";
                var groupedData = db.Database.SqlQuery<LowStockAlert>(query).ToList();
                LowStockEquipment = groupedData;
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }


        public void GetAllCost()
        {
            using var db = new InventoryManagmentEntities();
            try
            {
                var totalCost = db.Equipments.AsEnumerable().Sum(e => e.Cost);


                TotalCost = totalCost;
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }


        public void GetEquipmentCostByCategory()
        {
            using var db = new InventoryManagmentEntities();
            try
            {
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
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer,

                    ToolTipLabelFormatter =
                        point =>
                        {
                            var pv = point.Coordinate.PrimaryValue;
                            var sv = point.StackedValue!;

                            var a = $"{pv}/{sv.Total}{Environment.NewLine}{sv.Share:P2}";
                            return a;
                        },
                    MaxRadialColumnWidth = 60,
                    Values = new double[] { (double)(g.TotalCost ?? 0m) }, // Cast TotalCost to double
                    Name = g.CategoryName // Use dynamic category name
                }).ToArray();

                // Optionally, update the total equipment count if needed
                TotalEquipment = db.Equipments.Count();
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }

        public void GetTotalEquipment()
        {
            using var db = new InventoryManagmentEntities();
            try
            {
                TotalEquipment = db.Equipments.Count();
            }
            catch (Exception ex)
            {
                // Display an alert if an error occurs
                Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }

        public class LowStockAlert
        {
            public string CombinedMessage => $"{EquipmentName} is {StockStatus}";
            public string EquipmentName { get; set; }
            public int Quantity { get; set; }
            public string StockStatus { get; set; }
        }

        // This class represents the result of the raw SQL query
        public class CategoryCostSummary
        {
            public string CategoryName { get; set; }

            public decimal? TotalCost { get; set; }
        }
    }
}