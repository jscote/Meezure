using System;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Messaging;

namespace MeasureONE
{
	public class FilterViewModel : ViewModelBase
	{

		private string _loadingMsgId = string.Format ("Loading:{0}", MeasurementListPage.PageName.Remove (MeasurementListPage.PageName.IndexOf ("Page")));

		public FilterViewModel ()
		{
			this.FilterMeasures = new Command (() => ApplyFilter());
		}

		private void ApplyFilter() {
			var msg = new NotificationMessage<FilterViewModel> (this, _loadingMsgId);

			Messenger.Default.Send<NotificationMessage<FilterViewModel>> (msg, _loadingMsgId);
		}

		private string _subject;
		public string Subject {
			get {
				return _subject;
			}
			set {
				Set (() => Subject, ref _subject, value);

			}
		}

		public ICommand FilterMeasures { protected set; get; }
	}
}

