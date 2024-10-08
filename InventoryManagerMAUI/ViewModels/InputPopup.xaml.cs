using CommunityToolkit.Maui.Views;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels;

public partial class InputPopup : Popup
{

	private readonly List<Entry> _entries = new();

	public Dictionary<string, string> FieldResults { get; private set; } = new();

	EquipmentStatusViewModel q = new();
	public InputPopup(Dictionary<string, string> fieldLabels)
	{
		InitializeComponent();

		var stackLayout = new VerticalStackLayout
		{
			Padding = 20
		};
		foreach (var field in fieldLabels)
		{
			var label = new Label { Text = field.Key };
			var entry = new Entry { Placeholder = field.Value};

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
		
		BindingContext = q;
		
		okButton.Clicked += OnOkButtonClick;
		
		okButton.Command = q.AddCommand;
		
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
			FieldResults.Add($"Field{i+1}", _entries[i].Text);
		}
		Close(FieldResults);
	}

	private void OnCancelButtonClick(object sender, EventArgs e)
	{
		Close(null);
	}

}