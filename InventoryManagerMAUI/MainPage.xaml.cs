using InventoryManagerMAUI.View;

namespace InventoryManagerMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void EquipmentStatus_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EquipmentStatusView());
        }

        private void AuditsView_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AuditsView());
        }

        private void CategoriesView_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoriesView());
        }

        private void EquipmentView_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EquipmentsView());
        }

        private void LoginView_OnClicked(object? sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginView());
        }
    }
}