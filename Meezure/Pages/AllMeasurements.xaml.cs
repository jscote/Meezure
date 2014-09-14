using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using Autofac;
using GalaSoft.MvvmLight.Messaging;

namespace Meezure
{	
	public partial class AllMeasurements : MasterDetailPage
	{	
		private ILifetimeScope _scope;

		private string _loadingMsgId = string.Format ("Loading:{0}", MeasurementPage.PageName.Remove (MeasurementPage.PageName.IndexOf ("Page")));

		private FilterPage _master;
		public AllMeasurements ()
		{
			InitializeComponent ();

			Master = _master = new FilterPage ();

			_scope = App.AutoFacContainer.BeginLifetimeScope ();

			var mainNav = new NavigationPage (_scope.Resolve<MeasurementPage>());
			mainNav.Title = "Measurements";

			var msg = new NotificationMessage<IDictionary<string, int>>(new Dictionary<string, int>(), _loadingMsgId);
			msg.Content.Add("Mode", 0);

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

