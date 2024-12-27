using InventoryManagerMAUI.View;

namespace InventoryManagerMAUI
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new LoginView());
		}
	}
}
