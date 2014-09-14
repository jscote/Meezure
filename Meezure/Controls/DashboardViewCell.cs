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
			lblMeasurementType.HorizontalOptions = LayoutOptions.CenterAndExpand;

			var measurementLayout = new StackLayout ();

			foreach(var group in vm.MeasurementGroups) {
				var innerStack = new StackLayout ();
				innerStack.Orientation = StackOrientation.Vertical;
				measurementLayout.Children.Add (innerStack);

				var measurementStack = new StackLayout ();
				measurementStack.Orientation = StackOrientation.Vertical;
				measurementStack.HorizontalOptions = LayoutOptions.CenterAndExpand;
				innerStack.Children.Add (measurementStack);
				foreach (var item in group.MeasurementItems) {

					var innerMeasurementStack = new StackLayout ();
					if (group.MeasurementItems.Count > 1) {
						innerMeasurementStack.Children.Add (new Label () { Text = item.Name });
					}
					innerMeasurementStack.Orientation = StackOrientation.Horizontal;
					innerMeasurementStack.Children.Add (new Label () { Text = item.Value.ToString () });
					innerMeasurementStack.Children.Add (new Label () { Text = item.Uom });
					measurementStack.Children.Add (innerMeasurementStack);
				}

			}

			layout.Children.Add (lblMeasurementType);
			layout.Children.Add (measurementLayout);
		}
	}
}
