using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using GalaSoft.MvvmLight;

namespace MeasureONE
{
	public class Preference : ViewModelBase
	{
		public Preference ()
		{

			this.SavePreferences = new Command (() => {
				//Save the preferences to the model.
				this.UpdateDate = DateTimeOffset.UtcNow;
				//MessagingCenter.Send<BaseViewModel>(this, "Navigation::doneWithPreferences");
			});

		}



		private MeasurementSystem _measurementSystem = MeasurementSystem.Metric;
		public MeasurementSystem MeasurementSystem {
			get {
				return _measurementSystem;
			}
			set {

				Set (() => MeasurementSystem, ref _measurementSystem, value);
			}
		}

		public DateTimeOffset UpdateDate { get; protected set; }

		public ICommand SavePreferences { protected set; get; }
	}
}

