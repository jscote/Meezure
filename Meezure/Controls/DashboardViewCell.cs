using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Meezure
{
	public class DashboardViewCell: ViewCell
	{

		public DashboardViewCell ()
		{
		

		}

		protected override void OnBindingContextChanged ()
		{
			View = null;
			var layout = new StackLayout ();
			View = layout;

			var vm = BindingContext as MeasurementDashboardItemViewModel;

			layout.Orientation = StackOrientation.Vertical;
			layout.VerticalOptions = LayoutOptions.FillAndExpand;


			var measurementLayout = new StackLayout ();

			if (vm.MeasurementGroups != null) {


				var lblMeasurementType = new Label ();
				lblMeasurementType.SetBinding (Label.TextProperty, "MeasurementName");
				lblMeasurementType.HorizontalOptions = LayoutOptions.CenterAndExpand;
				layout.Children.Add (lblMeasurementType);

				foreach (var group in vm.MeasurementGroups) {
					var innerStack = new StackLayout ();
					innerStack.Orientation = StackOrientation.Vertical;
					measurementLayout.Children.Add (innerStack);

					var measurementStack = new StackLayout ();
					measurementStack.Orientation = StackOrientation.Vertical;
					measurementStack.HorizontalOptions = LayoutOptions.CenterAndExpand;
					innerStack.Children.Add (measurementStack);

					if (group.MeasurementItems != null) {
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

				}
			}


			layout.Children.Add (measurementLayout);

			var buttonStack = new StackLayout ();
			buttonStack.Orientation = StackOrientation.Horizontal;
			buttonStack.HorizontalOptions = LayoutOptions.CenterAndExpand;

			if (vm.MeasurementGroups != null) {
				var bStats = new Button ();
				bStats.Text = "More Stats";
				bStats.SetBinding (Button.CommandProperty, "GetDetails");
				bStats.CommandParameter = new Tuple<int, int> (vm.MeasurementSubjectId, vm.MeasurementDefinitionId);
				buttonStack.Children.Add (bStats);
			}

			var bAdd = new Button ();
			bAdd.Text = string.Format("Add {0}", vm.MeasurementName);
			bAdd.SetBinding (Button.CommandProperty, "AddMeasurement");
			IDictionary<string, int> parameters = new Dictionary<string, int> ();
			parameters.Add ("SubjectId", vm.MeasurementSubjectId);
			parameters.Add ("DefinitionId", vm.MeasurementDefinitionId);
			parameters.Add ("Mode", 0);
			bAdd.CommandParameter = parameters;


			buttonStack.Children.Add (bAdd);

			layout.Children.Add (buttonStack);

		}
	}
}
