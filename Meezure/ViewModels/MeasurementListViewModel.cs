using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MeasureONE
{
	public class MeasurementListViewModel: ViewModelBase
	{
		private string _loadingMsgId = string.Format ("Loading:{0}", MeasurementListPage.PageName.Remove (MeasurementListPage.PageName.IndexOf ("Page")));
		private IRepository<MeasurementInstanceModel> _repository;

		public MeasurementListViewModel (IRepository<MeasurementInstanceModel> repository)
		{
			_repository = repository;

			MessengerInstance.Register<NotificationMessage<IDictionary<string, int>>> (this, _loadingMsgId, (msg) => Load ());
			MessengerInstance.Register<NotificationMessage<FilterViewModel>> (this, _loadingMsgId, (msg) => Load (msg.Content));

		}

		private void Load(FilterViewModel filter) {



			string selectStr = "SELECT t1.* ";
			string fromStr = "FROM MeasurementInstanceModel AS t1";
			string whereStr = string.Empty;
			List<object> parameters = new List<object> ();

			if(!string.IsNullOrEmpty( filter.Subject)) {
				fromStr = fromStr + string.Format(" JOIN MeasurementSubjectModel AS t2 ON t1.MeasurementSubjectId == t2.Id");
				whereStr = whereStr + string.Format(" WHERE t2.Name == ?");
				parameters.Add(filter.Subject);
			}

			string query = selectStr + fromStr + whereStr;

			var measurements = _repository.GetAllWithChildren(query, parameters.ToArray());

			LoadList (measurements);
		}

		private void Load() {
			var measurements = _repository.GetAllWithChildren(predicate: null, orderBy: (o) => o.DateRecorded, descending: true, skip: null, count: null);
			LoadList (measurements);
		}

		private void LoadList(IList<MeasurementInstanceModel> measurements)
		{
			Items.Clear ();

			foreach (var entry in measurements) {
				var item = new MeasurementDashboardItemViewModel () {
					Subject = entry.Subject.Name,
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
	}
}

