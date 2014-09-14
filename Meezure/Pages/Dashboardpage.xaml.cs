using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Autofac;

namespace Meezure
{	
	public partial class DashboardPage : ContentPage
	{	
		private ILifetimeScope _scope;
		public static string PageName 
		{
			get 
			{
				return "DashboardPage";
			}
		}

		public DashboardPage ()
		{
			InitializeComponent ();
			_scope = App.AutoFacContainer.BeginLifetimeScope ();

			var viewModel = _scope.Resolve<MeasurementDashboardViewModel> ();

			BindingContext = viewModel;
		}

		protected override void OnBindingContextChanged ()
		{


			var vm = BindingContext as MeasurementDashboardViewModel;
			if (vm != null) {
				this.pickerSubject.Items.Clear ();
				foreach (var s in vm.Subjects) {
					this.pickerSubject.Items.Add (s.Name);
				}
			}

			base.OnBindingContextChanged ();


		}

	}
}

