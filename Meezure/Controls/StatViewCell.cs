using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Meezure
{
	public class StatViewCell: ViewCell
	{


		protected override void OnBindingContextChanged ()
		{
			View = null;
			var layout = new StackLayout ();


			var vm = BindingContext as MeasurementDashboardItemViewModel;

			layout.Orientation = StackOrientation.Vertical;
			layout.VerticalOptions = LayoutOptions.FillAndExpand;


			var lblMeasurementType = new Label ();
			lblMeasurementType.SetBinding (Label.TextProperty, "MeasurementName");
			lblMeasurementType.HorizontalOptions = LayoutOptions.CenterAndExpand;

			var measurementLayout = new StackLayout ();

			var itemCount = 0;

			foreach(var group in vm.MeasurementGroups) {
				var innerStack = new StackLayout ();
				innerStack.Orientation = StackOrientation.Vertical;
				measurementLayout.Children.Add (innerStack);

				var measurementStack = new StackLayout ();
				measurementStack.Orientation = StackOrientation.Vertical;
				measurementStack.HorizontalOptions = LayoutOptions.CenterAndExpand;
				innerStack.Children.Add (measurementStack);

				itemCount = itemCount + group.MeasurementItems.Count;

				foreach (var item in group.MeasurementItems) {



					var innerMeasurementStack = new StackLayout ();
					if (group.MeasurementItems.Count > 1) {
						innerMeasurementStack.Children.Add (new Label () { Text = item.Name });
					}
					innerMeasurementStack.Orientation = StackOrientation.Horizontal;
					innerMeasurementStack.Children.Add (new Label () { Text = item.Value.ToString () });
					innerMeasurementStack.Children.Add (new Label () { Text = item.Uom });
					innerMeasurementStack.HeightRequest = 40;
					measurementStack.Children.Add (innerMeasurementStack);
				}

			}

			layout.Children.Add (lblMeasurementType);
			layout.Children.Add (measurementLayout);

			View = layout;

			base.OnBindingContextChanged ();

			if (Device.OS == TargetPlatform.iOS) {
				Height = 20 + (itemCount * 40);
			}

		}
	}
}
