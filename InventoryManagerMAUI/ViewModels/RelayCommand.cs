using System.Windows.Input;

namespace InventoryManagerMAUI.ViewModels
{
	public class RelayCommand : ICommand
	{
		public event EventHandler? CanExecuteChanged;

		private Action<object> execute;
		private Func<object, bool> canExecute;

		public bool CanExecute(object? parameter)
		{
			return this.canExecute == null || this.canExecute(parameter);
		}
		
		 
		public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public void Execute(object? parameter)
		{
			this.execute(parameter);
		}
	}
}
