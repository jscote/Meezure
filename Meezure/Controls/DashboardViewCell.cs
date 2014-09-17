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

			var buttonStack = new StackLayout ();
			buttonStack.Orientation = StackOrientation.Horizontal;
			buttonStack.HorizontalOptions = LayoutOptions.CenterAndExpand;
			var bStats = new Button ();
			bStats.Text = "More Stats";
			bStats.SetBinding (Button.CommandProperty, "GetDetails");
			bStats.CommandParameter = new Tuple<int, int> (vm.MeasurementSubjectId, vm.MeasurementDefinitionId);

			var bAdd = new Button ();
			bAdd.Text = string.Format("Add {0}", vm.MeasurementName);
			bAdd.SetBinding (Button.CommandProperty, "AddMeasurement");
			bAdd.CommandParameter = new Tuple<int, int> (vm.MeasurementSubjectId, vm.MeasurementDefinitionId);

			buttonStack.Children.Add (bStats);
			buttonStack.Children.Add (bAdd);

			layout.Children.Add (buttonStack);

		}
	}
}
