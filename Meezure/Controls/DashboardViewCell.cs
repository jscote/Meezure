using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Meezure
{
	public class DashboardViewCell: ViewCell
	{

		public DashboardViewCell ()
		{
		

		}

		protected override void OnBindingContextChanged ()
		{
			var layout = new StackLayout ();
			View = layout;

			var vm = BindingContext as MeasurementDashboardItemViewModel;

			layout.Orientation = StackOrientation.Vertical;
			layout.VerticalOptions = LayoutOptions.FillAndExpand;

			var lblMeasurementType = new Label ();
			lblMeasurementType.SetBinding (Label.TextProperty, "MeasurementName");

			var measurementLayout = new StackLayout ();

			foreach(var group in vm.MeasurementGroups) {
				var innerStack = new StackLayout ();
				innerStack.Orientation = StackOrientation.Vertical;
				measurementLayout.Children.Add (innerStack);
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

			layout.Children.Add (lblMeasurementType);
			layout.Children.Add (measurementLayout);
		}
	}
}
