using System;
using Xamarin.Forms;

namespace Meezure
{
	public class MeasurementEntryCell : ViewCell
	{

		public MeasurementEntryCell ()
		{

			var grid = new Grid ();
			grid.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength (75, GridUnitType.Absolute) });
			grid.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength (75, GridUnitType.Star) });
			grid.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength (50, GridUnitType.Star) });

			grid.RowDefinitions.Add (new RowDefinition () { Height = new GridLength (40, GridUnitType.Auto) });

			grid.RowSpacing = 0;

			var label = new Label ();
			label.SetBinding (Xamarin.Forms.Label.TextProperty, "Name");
			label.YAlign = TextAlignment.Center;
			label.XAlign = TextAlignment.End;

			var entry = new Entry ();
			entry.SetBinding (Xamarin.Forms.Entry.TextProperty, "Value");
			entry.Keyboard = Keyboard.Numeric;


			var labelUom = new Label ();
			labelUom.SetBinding (Xamarin.Forms.Label.TextProperty, "Uom");
			labelUom.YAlign = TextAlignment.Center;
			labelUom.XAlign = TextAlignment.Start;

			/*var pickerUom = new Picker ();
			pickerUom.Items.Add ("mmg");
			pickerUom.Items.Add ("mmHg");
			pickerUom.SelectedIndex = 1;
			//Todo, still need to bind the result to a property of the model*/

			grid.Children.Add (label, 0, 0);
			grid.Children.Add (entry, 1, 0);
			grid.Children.Add (labelUom, 2, 0);

			View = grid;

			base.OnBindingContextChanged ();

			if (Device.OS == TargetPlatform.iOS) {
				Height = grid.RowDefinitions.Count * 50;
			}

		}
			
	}
}

