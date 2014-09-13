using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;

namespace MeasureONE
{
	public class MeasurementDashboardViewModel: ViewModelBase
	{

		private string _loadingMsgId = string.Format ("Loading:Dashboard");

		private IRepository<MeasurementInstanceModel> _repository;

		public MeasurementDashboardViewModel(IRepository<MeasurementInstanceModel> repository) 
		{
			_repository = repository;

			MessengerInstance.Register<NotificationMessage> (this, _loadingMsgId, (msg) => Load ());

		}


		public void Load() {
			var measurements = _repository.GetAllWithChildren(predicate: null, orderBy: (o) => o.DateRecorded, descending: true, skip: 0, count: 3);

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

		public void Init (bool inMainMenu)
		{
			Items.Clear ();

			var measures = _repository.GetAll ();

			if (inMainMenu) {
				Items.Add (new MeasurementDashboardItemViewModel () {
					MeasurementDefinitionId = (int)MeasurementTypeDefId.BloodPressure,
					Subject = "JS",
					LastRecordedDate = DateTime.Now,
					MeasurementName = "Blood Pressure",
					MeasurementGroups = new ObservableCollection<MeasurementItemGroupViewModel>() {
						new MeasurementItemGroupViewModel () {
							DefinitionName = "Blood Pressure",
							MeasurementType = "Last Entry",
							MeasurementItems = new ObservableCollection<MeasurementItemViewModel> () {
								new MeasurementItemViewModel () {
									Name = "Systolic",
									Value = 100,
									Uom = "mmg"
								},
								new MeasurementItemViewModel () {
									Name = "Diastolic",
									Value = 60,
									Uom = "mmg"
								}
							}
						}
	
					}
				});

				Items.Add (new MeasurementDashboardItemViewModel () {
					MeasurementDefinitionId = (int)MeasurementTypeDefId.Dimension,
					Subject = "Living Room Window",
					LastRecordedDate = DateTime.Now,
					MeasurementName = "Dimension",
					MeasurementGroups = new ObservableCollection<MeasurementItemGroupViewModel>() {
						new MeasurementItemGroupViewModel () {
							DefinitionName = "Dimension",
							MeasurementType = "Last Entry",
							MeasurementItems = new ObservableCollection<MeasurementItemViewModel> () {
								new MeasurementItemViewModel () {
									Name = "Width",
									Value = 60,
									Uom = "in"
								},
								new MeasurementItemViewModel () {
									Name = "Height",
									Value = 60,
									Uom = "in"
								}
							}
						}
					}
				});
				return;
			}
			Items.Add (new MeasurementDashboardItemViewModel () {
				MeasurementDefinitionId = (int)MeasurementTypeDefId.BloodPressure,
				Subject = "JS",
				LastRecordedDate = DateTime.Now,
				MeasurementName = "Blood Pressure",
				MeasurementGroups = new ObservableCollection<MeasurementItemGroupViewModel>() {
					new MeasurementItemGroupViewModel () {
						DefinitionName = "Blood Pressure",
						MeasurementType = "Minimum Goal",
						MeasurementItems = new ObservableCollection<MeasurementItemViewModel> () {
							new MeasurementItemViewModel () {
								Name = "Systolic",
								Value = 100,
								Uom = "mmg"
							},
							new MeasurementItemViewModel () {
								Name = "Diastolic",
								Value = 60,
								Uom = "mmg"
							}
						}
					},
					new MeasurementItemGroupViewModel () {
						DefinitionName = "Blood Pressure",
						MeasurementType = "Maximum Goal",
						MeasurementItems = new ObservableCollection<MeasurementItemViewModel> () {
							new MeasurementItemViewModel () {
								Name = "Systolic",
								Value = 120,
								Uom = "mmg"
							},
							new MeasurementItemViewModel () {
								Name = "Diastolic",
								Value = 90,
								Uom = "mmg"
							}
						}
					},
					new MeasurementItemGroupViewModel () {
						DefinitionName = "Blood Pressure",
						MeasurementType = "Average",
						MeasurementItems = new ObservableCollection<MeasurementItemViewModel> () {
							new MeasurementItemViewModel () {
								Name = "Systolic",
								Value = 123,
								Uom = "mmg"
							},
							new MeasurementItemViewModel () {
								Name = "Diastolic",
								Value = 86,
								Uom = "mmg"
							}
						}
					},
					new MeasurementItemGroupViewModel () {
						DefinitionName = "Blood Pressure",
						MeasurementType = "Maximum",
						MeasurementItems = new ObservableCollection<MeasurementItemViewModel> () {
							new MeasurementItemViewModel () {
								Name = "Systolic",
								Value = 133,
								Uom = "mmg"
							},
							new MeasurementItemViewModel () {
								Name = "Diastolic",
								Value = 94,
								Uom = "mmg"
							}
						}
					},
					new MeasurementItemGroupViewModel () {
						DefinitionName = "Blood Pressure",
						MeasurementType = "Minimum",
						MeasurementItems = new ObservableCollection<MeasurementItemViewModel> () {
							new MeasurementItemViewModel () {
								Name = "Systolic",
								Value = 111,
								Uom = "mmg"
							},
							new MeasurementItemViewModel () {
								Name = "Diastolic",
								Value = 76,
								Uom = "mmg"
							}
						}
					}
				}
			});

			Items.Add (new MeasurementDashboardItemViewModel () {
				MeasurementDefinitionId = (int)MeasurementTypeDefId.Dimension,
				Subject = "Living Room Window",
				LastRecordedDate = DateTime.Now,
				MeasurementName = "Dimension",
				MeasurementGroups = new ObservableCollection<MeasurementItemGroupViewModel>() {
					new MeasurementItemGroupViewModel () {
						DefinitionName = "Dimension",
						MeasurementType = "Dimension",
						MeasurementItems = new ObservableCollection<MeasurementItemViewModel> () {
							new MeasurementItemViewModel () {
								Name = "Width",
								Value = 60,
								Uom = "in"
							},
							new MeasurementItemViewModel () {
								Name = "Height",
								Value = 60,
								Uom = "in"
							}
						}
					}
				}
			});
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

	public class MeasurementItemViewModel : ViewModelBase
	{
		public MeasurementItemViewModel () 
		{

		}

		private int _definitionId;
		public int DefinitionId { 
			get {
				return _definitionId;
			}
			set {
				Set (() => DefinitionId, ref _definitionId, value);
			}
		}

		private decimal _value;
		public decimal Value { 
			get {
				return _value;
			}
			set {
				Set (() => Value, ref _value, value);
			}
		}

		private string _uom;
		public string Uom { 
			get {
				return _uom;
			}
			set {
				Set (() => Uom, ref _uom, value);
			}
		}

		private int _uomId;
		public int UomId { 
			get {
				return _uomId;
			}
			set {
				Set (() => UomId, ref _uomId, value);
			}
		}

		private string _name;
		public string Name { 
			get {
				return _name;
			}
			set {
				Set (() => Name, ref _name, value);
			}
		}
	}

	public class MeasurementItemGroupViewModel :ViewModelBase
	{
		public MeasurementItemGroupViewModel() {
			_measurementItems = new ObservableCollection<MeasurementItemViewModel> ();
		}

		private int _definitionId;
		public int DefinitionId { 
			get {
				return _definitionId;
			}
			set {
				Set (() => DefinitionId, ref _definitionId, value);
			}
		}

		private int _subjectId;
		public int SubjectId { 
			get {
				return _subjectId;
			}
			set {
				Set (() => SubjectId, ref _subjectId, value);
			}
		}

		private string _definitionName;
		public string DefinitionName { 
			get {
				return _definitionName;
			}
			set {
				Set (() => DefinitionName, ref _definitionName, value);
			}
		}

		private string _subjectName;
		public string SubjectName { 
			get {
				return _subjectName;
			}
			set {
				Set (() => SubjectName, ref _subjectName, value);
			}
		}

		private string _measurementType;
		public string MeasurementType { 
			get {
				return _measurementType;
			}
			set {
				Set (() => MeasurementType, ref _measurementType, value);
			}
		}

		private ObservableCollection<MeasurementItemViewModel> _measurementItems;
		public ObservableCollection<MeasurementItemViewModel> MeasurementItems 
		{
			get
			{
				return _measurementItems;
			}
			set
			{
				_measurementItems = value;
			}
			}
	}

	public class MeasurementDashboardItemViewModel: ViewModelBase
	{

		public MeasurementDashboardItemViewModel ()
		{
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

		private string _measurementName;
		public string MeasurementName { 
			get {
				return _measurementName;
			}
			set {
				Set (() => MeasurementName, ref _measurementName, value);

			}
		}

		private ObservableCollection<MeasurementItemGroupViewModel> _measurementGroups;
		public ObservableCollection<MeasurementItemGroupViewModel> MeasurementGroups { 
			get {
				return _measurementGroups;
			}
			set {
				Set (() => MeasurementGroups, ref _measurementGroups, value);

			}
		}


		private DateTime _lastRecordedDate;
		public DateTime LastRecordedDate { 
			get {
				return _lastRecordedDate;
			}
			set {
				Set (() => LastRecordedDate, ref _lastRecordedDate, value);

			}
		}

		private int _measurementDefinitionId;
		public int MeasurementDefinitionId { 
			get {
				return _measurementDefinitionId;
			}
			set {
				Set (() => MeasurementDefinitionId, ref _measurementDefinitionId, value);

			}
		}

	}
}

