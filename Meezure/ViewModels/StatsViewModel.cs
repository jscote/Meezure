using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Autofac;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Meezure
{
	public class StatsViewModel : ViewModelBase
	{

		private string _loadingMsgId = string.Format ("Loading:{0}", StatsPage.PageName.Remove (StatsPage.PageName.IndexOf ("Page")));

		public ICommand ClosePopup { get; set; }

		public StatsViewModel (){
			MessengerInstance.Register<NotificationMessage<Tuple<int, int>>> (this, _loadingMsgId, (msg) => Load (msg.Content));
			this.ClosePopup = new Command (() => Close());
		}

		private void Close() {
			App.NavigationService.NavigateBack ();
		}

		private void Load(Tuple<int, int> parameter) {
			Items.Clear ();

			var measurementTypes = new List<string> () { "Average", "Minimum observed", "Maximum observed", "Last Entry" };

			IList<MeasurementInstanceModel> measurements = new List<MeasurementInstanceModel> ();

			using(var scope = App.AutoFacContainer.BeginLifetimeScope ()) {	
				foreach (var type in measurementTypes) {
					var query = scope.ResolveKeyed<IPredefinedQuery<MeasurementInstanceModel>> (type);
					var result = query.Query ((new List<object>() {parameter.Item1, parameter.Item2}).ToArray());
					if (result != null) {

						var str = result.Definition.Name;
						MeasurementName = str;
						result.Definition.Name = type;
						measurements.Add (result);
					}
				}
			}


			foreach (var entry in measurements) {
				var item = new MeasurementDashboardItemViewModel () {
					Subject = entry.Subject.Name,
					MeasurementSubjectId = entry.MeasurementSubjectId,
					LastRecordedDate = entry.DateRecorded,
					MeasurementName = entry.Definition.Name,
					MeasurementDefinitionId = entry.MeasurementDefinitionId
				};

				if(entry.MeasurementGroups != null && entry.MeasurementGroups.Any()) {

					item.MeasurementGroups = new ObservableCollection<MeasurementItemGroupViewModel> ();
					var g = new MeasurementItemGroupViewModel ();
					item.MeasurementGroups.Add (g);
					g.MeasurementItems = new ObservableCollection<MeasurementItemViewModel> ();
					foreach (var group in entry.MeasurementGroups) {

						g.DefinitionName = group.MeasurementGroupDefinitionModel.Name;
						g.MeasurementItems.Add (new MeasurementItemViewModel () { 
							Name = group.MeasurementGroupDefinitionModel.Name,
							Uom = group.Unit.Abbreviation,
							Value = group.Value
						});

					}

				}

				Items.Add (item);
			}

		}

		private ObservableCollection<MeasurementDashboardItemViewModel> _items;
		public ObservableCollection<MeasurementDashboardItemViewModel> Items {
			get{ 
				if (_items == null) {
					_items = new ObservableCollection<MeasurementDashboardItemViewModel> ();
				}

				return _items;
			}
		}

		private string _measurementName;
		public string MeasurementName {			
			get {
				return _measurementName;
			}
			set {
				Set (() => MeasurementName, ref _measurementName, value);
			}
		}
	}
}

