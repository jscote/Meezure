using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Autofac;

namespace MeasureONE
{	
	public partial class HomePage : ContentPage
	{	

		private ILifetimeScope _scope;

		public static string PageName 
		{
			get 
			{
				return "HomePage";
			}
		}

		public HomePage()
		{
			InitializeComponent ();

			_scope = App.AutoFacContainer.BeginLifetimeScope ();

			var viewModel = _scope.Resolve<MeasurementDashboardViewModel> ();

			BindingContext = viewModel;

			this.SetToolbarBindingContext ();
		}


		void OnSelectedMeasurement(object sender, ItemTappedEventArgs args)
		{
			App.NavigationService.OpenModal (MeasurementPage.PageName, (args.Item as MeasurementDashboardItemViewModel).MeasurementDefinitionId);
		}
	}
}

