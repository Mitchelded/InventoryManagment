using InventoryManagerMAUI.View;

namespace InventoryManagerMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Status_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StatusView());
        }

        private void AuditsView_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AuditsView());
        }

        private void CategoryView_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryView());
        }

        private void EquipmentView_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EquipmentsView());
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
    }
}