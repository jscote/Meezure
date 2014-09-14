using System;
using Xamarin.Forms;

namespace Meezure
{
	public class MeasurementStatGrid : ContentView
	{
		public MeasurementStatGrid ()
		{

		}

		protected override void OnBindingContextChanged ()
		{
			var vm = BindingContext as MeasurementViewModel;
			var grid = new Grid ();

			grid.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength (5, GridUnitType.Auto) });
			grid.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength (5, GridUnitType.Star) });
			grid.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength (5, GridUnitType.Star) });
			grid.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength (5, GridUnitType.Star) });
			grid.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength (5, GridUnitType.Star) });
			grid.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength (5, GridUnitType.Star) });
			grid.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength (5, GridUnitType.Star) });

			grid.RowDefinitions.Add (new RowDefinition () { Height = new GridLength (100, GridUnitType.Auto) });
			grid.RowDefinitions.Add (new RowDefinition () { Height = new GridLength (100, GridUnitType.Star) });
			grid.RowDefinitions.Add (new RowDefinition () { Height = new GridLength (100, GridUnitType.Star) });

			grid.Children.Add (new Label () { Text = "Avg", Rotation = 0, XAlign = TextAlignment.Center }, 1, 0);
			grid.Children.Add (new Label () { Text = "Min", Rotation = 0, XAlign = TextAlignment.Center }, 2, 0);
			grid.Children.Add (new Label () { Text = "Max", Rotation = 0, XAlign = TextAlignment.Center }, 3, 0);
			grid.Children.Add (new Label () { Text = "Goal Min", Rotation = 0, XAlign = TextAlignment.Center }, 4, 0);
			grid.Children.Add (new Label () { Text = "Goal Max", Rotation = 0, XAlign = TextAlignment.Center }, 5, 0);

			grid.Children.Add (new Label () {Text = "Diastolic" }, 0, 1);
			grid.Children.Add (new Label () {Text = "Systolic" }, 0, 2);

			grid.Children.Add (new Label () { Text = "120", XAlign = TextAlignment.Center }, 1, 1);
			grid.Children.Add (new Label () { Text = "120", XAlign = TextAlignment.Center  }, 2, 1);
			grid.Children.Add (new Label () { Text = "120", XAlign = TextAlignment.Center  }, 3, 1);
			grid.Children.Add (new Label () { Text = "120", XAlign = TextAlignment.Center  }, 4, 1);
			grid.Children.Add (new Label () { Text = "120", XAlign = TextAlignment.Center  }, 5, 1);

			grid.Children.Add (new Label () { Text = "80", XAlign = TextAlignment.Center }, 1, 2);
			grid.Children.Add (new Label () { Text = "80", XAlign = TextAlignment.Center  }, 2, 2);
			grid.Children.Add (new Label () { Text = "80", XAlign = TextAlignment.Center  }, 3, 2);
			grid.Children.Add (new Label () { Text = "80", XAlign = TextAlignment.Center  }, 4, 2);
			grid.Children.Add (new Label () { Text = "80", XAlign = TextAlignment.Center  }, 5, 2);


			Content = grid;

		}
	}
}

