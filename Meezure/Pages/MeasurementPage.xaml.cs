using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Messaging;
using Autofac;
using System.Collections;

namespace Meezure
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

		protected override void OnAppearing ()
		{
			//base.OnBindingContextChanged ();

			var lView = Content.FindByName<ListView> ("list");


			var vm = (BindingContext as MeasurementViewModel);

			if (vm != null) {
				if (vm.MeasurementGroup != null && vm.MeasurementGroup.MeasurementItems != null ) {

					lView.HeightRequest = vm.MeasurementGroup.MeasurementItems.Count * 60;
				}
			}


			base.OnAppearing ();
		}

	}
}

