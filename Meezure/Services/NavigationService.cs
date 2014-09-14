using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using Autofac;


namespace Meezure
{
	public class NavigationService
	{
		public INavigation Navigation { get; set; }


		public NavigationService ()
		{
			Initialize ();
		}

		private void Initialize()
		{
		}

		private Page Current { get; set; }

		public Task NavigateTo<T>(string whereTo, T data)
		{
	
			SetCurrentPage (whereTo);

			var msgId = string.Format ("Loading:{0}", whereTo.Remove (whereTo.IndexOf ("Page")));
			var msg = new NotificationMessage<T> (data, msgId);
			Messenger.Default.Send<NotificationMessage<T>> (msg, msgId);
			return Navigation.PushAsync (Current);
		}

		public Page MasterNavigateTo<T>(string whereTo, T data)
		{

			SetCurrentPage (whereTo);

			var msgId = string.Format ("Loading:{0}", whereTo.Remove (whereTo.IndexOf ("Page")));
			var msg = new NotificationMessage<T> (data, msgId);
			Messenger.Default.Send<NotificationMessage<T>> (msg, msgId);
			return Current;
		}

		public Task NavigateTo(string whereTo)
		{

			SetCurrentPage (whereTo);
			return Navigation.PushAsync (Current);
		}

		public Task NavigateBack() {

			return Navigation.PopAsync ();
		}

		public Task OpenModal(string whereTo)
		{
			SetCurrentPage (whereTo);
			return Navigation.PushModalAsync (Current);
		}

		public Task OpenModal<T>(string whereTo, T data)
		{
			SetCurrentPage (whereTo);

			var msgId = string.Format ("OpenModal:{0}", whereTo.Remove (whereTo.IndexOf ("Page")));
			var msg = new NotificationMessage<T> (data, msgId);
			Messenger.Default.Send<NotificationMessage<T>> (msg, msgId);

			return Navigation.PushModalAsync (Current);
		}

		public Task CloseModal()
		{
			return Navigation.PopModalAsync ();
		}

		private void SetCurrentPage(string whereTo)
		{
			if (whereTo == PreferencesPage.PageName) {
				Current = App.AutoFacContainer.Resolve<PreferencesPage> ();
			}

			if (whereTo == MeasurementPage.PageName) {
				Current = App.AutoFacContainer.Resolve<MeasurementPage> ();
			}

			/*if (whereTo == HomePage.PageName) {
				Current = App.AutoFacContainer.Resolve<HomePage> ();
			}*/

		}
	}
}

