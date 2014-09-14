using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Meezure
{	
	public partial class PreferencesPage : ContentPage
	{	
		public static string PageName 
		{
			get 
			{
				return "PreferencesPage";
			}
		}
			

		public PreferencesPage (Preference viewModel)
		{
			BindingContext = viewModel;
			InitializeComponent ();

		}

		async void GoToUnits(object sender, EventArgs e){
			await Navigation.PushAsync (new UnitListPage ());
		}

		async void GoToMeasurementTypes(object sender, EventArgs e){
			await Navigation.PushAsync (new MeasurementTypesPage ());
		}
	}
}

