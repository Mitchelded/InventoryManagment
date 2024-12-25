using InventoryManagerMAUI.View;

namespace InventoryManagerMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LoginView_OnClicked(object? sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginView());
        }

        private void InventoryView_OnClicked(object? sender, EventArgs e)
        {
            Navigation.PushAsync(new InventoryView());
        }

        private void DashboardView_OnClicked(object? sender, EventArgs e)
        {
            Navigation.PushAsync(new DashboardView());
        }

        private void OrderView_OnClicked(object? sender, EventArgs e)
        {
            Navigation.PushAsync(new OrdersManagementView());
        }

        private void EquipmentMovementView_OnClicked(object? sender, EventArgs e)
        {
            Navigation.PushAsync(new EquipmentMovementView());
        }
    }
}