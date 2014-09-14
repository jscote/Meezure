using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Autofac;

namespace Meezure
{	
	public partial class StatsPage : ContentPage
	{	



		private ILifetimeScope _scope;
		public static string PageName 
		{
			get 
			{
				return "StatsPage";
			}
		}

		public StatsPage ()
		{
			InitializeComponent ();

			_scope = App.AutoFacContainer.BeginLifetimeScope ();

			var viewModel = _scope.Resolve<StatsViewModel> ();

			BindingContext = viewModel;



		}

	}
}

