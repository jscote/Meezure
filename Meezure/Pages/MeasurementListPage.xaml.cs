using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Autofac;

namespace MeasureONE
{	
	public partial class MeasurementListPage : ContentPage
	{	
		private ILifetimeScope _scope;
		public static string PageName 
		{
			get 
			{
				return "MeasurementListPage";
			}
		}

		public MeasurementListPage ()
		{
			InitializeComponent ();
			_scope = App.AutoFacContainer.BeginLifetimeScope ();

			var viewModel = _scope.Resolve<MeasurementListViewModel> ();

			BindingContext = viewModel;
		}
	}
}

