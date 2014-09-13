using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using Autofac;
using GalaSoft.MvvmLight.Messaging;

namespace MeasureONE
{	
	public partial class AllMeasurements : MasterDetailPage
	{	
		private ILifetimeScope _scope;

		private string _loadingMsgId = string.Format ("Loading:{0}", MeasurementListPage.PageName.Remove (MeasurementListPage.PageName.IndexOf ("Page")));

		private FilterPage _master;
		public AllMeasurements ()
		{
			InitializeComponent ();

			Master = _master = new FilterPage ();

			_scope = App.AutoFacContainer.BeginLifetimeScope ();

			var mainNav = new NavigationPage (_scope.Resolve<MeasurementListPage>());
			mainNav.Title = "Measurements";

			var msg = new NotificationMessage<IDictionary<string, int>>(new Dictionary<string, int>(), _loadingMsgId);

			Messenger.Default.Send<NotificationMessage<IDictionary<string, int>>> (msg, _loadingMsgId);

			Detail = mainNav;

			this.Icon = "slideout.png";

			Messenger.Default.Register<NotificationMessage<FilterViewModel>>(this, _loadingMsgId, m => IsPresented = false);


			/*_master.PageSelectionChanged = (measurementTypeDefId) => {

				IDictionary<string, int> msgSelectionChanged = new Dictionary<string, int>();
				msgSelectionChanged.Add("DefinitionId", measurementTypeDefId);
				msgSelectionChanged.Add("Mode", 0);
				var page = App.NavigationService.MasterNavigateTo (MeasurementListPage.PageName, msgSelectionChanged);
				Detail = new NavigationPage(page);
				Detail.Title = page.Title;
				IsPresented = false;
			};*/
		}

	}

}

