using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerMAUI.ViewModels
{
	partial class InputPopup<T> : BasePopup where T : class
	{
		public InputPopup(Dictionary<string, string> fieldLabels, T viewModel) : base()
		{
			GeneratePopup(fieldLabels, viewModel);
		}
		private readonly List<Entry> _entries = new();

		public Dictionary<string, string> FieldResults { get; private set; } = new();

		private void GeneratePopup(Dictionary<string, string> fieldLabels, T viewModel)
		{
			var stackLayout = new VerticalStackLayout
			{
				Padding = 20
			};
			foreach (var field in fieldLabels)
			{
				var label = new Label { Text = field.Key };
				var entry = new Entry { Placeholder = field.Value };

				entry.SetBinding(Entry.TextProperty, field.Value);

				_entries.Add(entry);
				stackLayout.Add(label);
				stackLayout.Add(entry);
			}
			var buttonsLayout = new HorizontalStackLayout
			{
				Spacing = 10,
				HorizontalOptions = LayoutOptions.End,
				Margin = new Thickness(0, 20, 0, 0)
			};

			var okButton = new Button
			{
				Text = "OK",
			};

			BindingContext = viewModel;

			okButton.Clicked += OnOkButtonClick;

			okButton.SetBinding(Button.CommandProperty, "AddCommand");

			//okButton.Command = viewModel.AddCommand;

			var cancelButton = new Button { Text = "Cancel", };

			cancelButton.Clicked += OnCancelButtonClick;

			buttonsLayout.Add(okButton);
			buttonsLayout.Add(cancelButton);
			stackLayout.Add(buttonsLayout);

			Content = stackLayout;
		}

		private void OnOkButtonClick(object sender, EventArgs e)
		{

			for (int i = 0; i < _entries.Count; i++)
			{
				FieldResults.Add($"Field{i + 1}", _entries[i].Text);
			}
			Close(FieldResults);
		}

		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			Close(null);
		}
	}
}
