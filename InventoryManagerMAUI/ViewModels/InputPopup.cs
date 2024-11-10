using InventoryManagerMAUI.Commands;
using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels
{
	class InputPopup<T> : BasePopup where T : class
	{
		private readonly T _viewModel;

		public InputPopup(Dictionary<string, string> fieldLabels, T viewModel) : base()
		{
			_viewModel = viewModel;
			GeneratePopup(fieldLabels, viewModel);
		}
		private readonly List<Entry> _entries = new();
		private readonly List<DatePicker> _datepickers = new();
		private readonly List<Picker> _pickers = new();

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
				Entry entry;
				DatePicker datepicker;
				Picker picker;
				if (field.Value.ToLower().Contains("date") || field.Value.ToLower().Contains("expirat"))
				{
					datepicker = new DatePicker{};
					datepicker.SetBinding(DatePicker.DateProperty, field.Value);
					_datepickers.Add(datepicker);
					stackLayout.Add(label);
					stackLayout.Add(datepicker);
				}
				else if (field.Value.ToLower().Contains("id"))
				{
					using InventoryManagmentEntities db = new();
					switch (field.Value)
					{
						case { } value when value.Contains("Department"):
							picker = new Picker();
							picker.SetBinding(Picker.ItemsSourceProperty, "Departments");
							picker.SetBinding(Picker.SelectedItemProperty, "SelectedDepartment");
							picker.ItemDisplayBinding = new Binding("Name");
							picker.ItemsSource = db.Departments.ToList();
							_pickers.Add(picker);
							stackLayout.Add(label);
							stackLayout.Add(picker);
							break;
						case { } value when value.Contains("Supplier"):
							picker = new Picker();
							picker.SetBinding(Picker.ItemsSourceProperty, "Suppliers");
							picker.SetBinding(Picker.SelectedItemProperty, "SelectedSupplier");
							picker.ItemDisplayBinding = new Binding("Name");
							picker.ItemsSource = db.Suppliers.ToList();
							_pickers.Add(picker);
							stackLayout.Add(label);
							stackLayout.Add(picker);
							break;
						case { } value when value.Contains("Location"):
							picker = new Picker();
							picker.SetBinding(Picker.ItemsSourceProperty, "Locations");
							picker.SetBinding(Picker.SelectedItemProperty, "SelectedLocation");
							picker.ItemDisplayBinding = new Binding("Description");
							picker.ItemsSource = db.Locations.ToList();
							_pickers.Add(picker);
							stackLayout.Add(label);
							stackLayout.Add(picker);
							break;
						case { } value when value.Contains("Category"):
							picker = new Picker();
							picker.SetBinding(Picker.ItemsSourceProperty, "Categories");
							picker.SetBinding(Picker.SelectedItemProperty, "SelectedCategory");
							picker.ItemDisplayBinding = new Binding("Name");
							picker.ItemsSource = db.Categories.ToList();
							_pickers.Add(picker);
							stackLayout.Add(label);
							stackLayout.Add(picker);
							break;
						case { } value when value.Contains("Status"):
							picker = new Picker();
							picker.SetBinding(Picker.ItemsSourceProperty, "EquipmentStatus");
							picker.SetBinding(Picker.SelectedItemProperty, "SelectedStatus");
							picker.ItemDisplayBinding = new Binding("Name");
							picker.ItemsSource = db.EquipmentStatus.ToList();
							_pickers.Add(picker);
							stackLayout.Add(label);
							stackLayout.Add(picker);
							break;
					}
				}
				else if (field.Value.ToLower().Contains("serial"))
				{
					
					entry = new Entry { Placeholder = field.Value};
					entry.SetBinding(Entry.TextProperty, field.Value);
					_entries.Add(entry);
					stackLayout.Add(label);
					stackLayout.Add(entry);
				}
				else
				{
					entry = new Entry { Placeholder = field.Value };
					entry.SetBinding(Entry.TextProperty, field.Value);
					_entries.Add(entry);
					stackLayout.Add(label);
					stackLayout.Add(entry);
				}
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
				for(int j = 0 ; j < _datepickers.Count; j++)
				{
					if (_datepickers[j] == null && _datepickers[j] is DatePicker)
					{
						FieldResults.Add($"Field{i + 1}", _datepickers[i].Date.ToString());
					}
				}
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
