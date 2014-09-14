using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Meezure
{
	public class DashboardViewCell: ViewCell
	{
		static StackLayout ConvertToMeasurementGroupItem (StackLayout parentLayout, string path)
		{
			var layout = new StackLayout();
			var converter = new MeasurementGroupItemConverter ();
			var binding = new Binding ();

			var lbl = new Label ();

			binding.Path = path;
			binding.Converter = converter;
			binding.ConverterParameter = layout;
			lbl.SetBinding (Label.TextProperty, binding);
			lbl.IsVisible = false;

			parentLayout.Children.Add (lbl);

			layout.Orientation = StackOrientation.Vertical;
			layout.VerticalOptions = LayoutOptions.FillAndExpand;

			return layout;
		}

		public DashboardViewCell ()
		{
		
			var layout = new StackLayout ();
			View = layout;

			layout.Orientation = StackOrientation.Vertical;
			layout.VerticalOptions = LayoutOptions.FillAndExpand;

			var lblSubject = new Label ();
			lblSubject.SetBinding (Label.TextProperty, "Subject");

			var lblEnteredOn = new Label ();
			lblEnteredOn.SetBinding (Label.TextProperty, "LastRecordedDate");

			var lblMeasurementType = new Label ();
			lblMeasurementType.SetBinding (Label.TextProperty, "MeasurementName");


			var measurementLayout = ConvertToMeasurementGroupItem (layout, "MeasurementGroups");

			layout.Children.Add (lblSubject);
			layout.Children.Add (lblEnteredOn);
			layout.Children.Add (lblMeasurementType);
			layout.Children.Add (measurementLayout);
		}
	}


	public class MeasurementGroupItemConverter : IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var val = value as ObservableCollection<MeasurementItemGroupViewModel>;
			if (val == null)
				return null;

			var stack = parameter as StackLayout;


			if (stack != null) {
				foreach(var group in val) {
					var innerStack = new StackLayout ();
					innerStack.Orientation = StackOrientation.Vertical;
					stack.Children.Add (innerStack);
					innerStack.Children.Add (new Label () {Text = group.MeasurementType });

					var measurementStack = new StackLayout ();
					measurementStack.Orientation = StackOrientation.Horizontal;
					innerStack.Children.Add (measurementStack);
					foreach (var item in group.MeasurementItems) {
						measurementStack.Children.Add (new Label () { Text = item.Name });
						measurementStack.Children.Add (new Label () { Text = item.Value.ToString () });
						measurementStack.Children.Add (new Label () { Text = item.Uom });
					}

				}
			}


			return null;
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion


	}
}

