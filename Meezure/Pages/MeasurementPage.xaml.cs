using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Messaging;
using Autofac;
using System.Collections;

namespace MeasureONE
{	
	public partial class MeasurementPage : ContentPage
	{	



		private ILifetimeScope _scope;
		public static string PageName 
		{
			get 
			{
				return "MeasurementPage";
			}
		}

		public MeasurementPage ()
		{
			InitializeComponent ();

			_scope = App.AutoFacContainer.BeginLifetimeScope ();

			var viewModel = _scope.Resolve<MeasurementViewModel> ();

			BindingContext = viewModel;



		}


	}
}

