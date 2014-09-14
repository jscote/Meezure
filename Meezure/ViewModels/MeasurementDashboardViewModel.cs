using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;
using Autofac;
using System.Collections.Generic;

namespace Meezure
{
	public class MeasurementDashboardViewModel: ViewModelBase
	{

		private string _loadingMsgId = string.Format ("Loading:Dashboard");

		private IRepository<MeasurementInstanceModel> _repository;
		private IRepository<MeasurementSubjectModel> _subjectRepository;
		private IRepository<ProfileModel> _profileRepository;

		public MeasurementDashboardViewModel(IRepository<ProfileModel> profileRepository, IRepository<MeasurementInstanceModel> repository, IRepository<MeasurementSubjectModel> subjectRepository) 
		{
			_subjectRepository = subjectRepository;
			_repository = repository;
			_profileRepository = profileRepository;

			Subjects = new ObservableCollection<MeasurementSubjectModel> (_subjectRepository.GetAll ());

			//MessengerInstance.Register<NotificationMessage> (this, _loadingMsgId, (msg) => Load ());

		}


		public void Load(int id) {


			IList<MeasurementInstanceModel> measurements = new List<MeasurementInstanceModel> ();//_repository.GetAllWithChildren(predicate: p => p.MeasurementSubjectId == id, orderBy: (o) => o.DateRecorded, descending: true, skip: 0, count: null);

			var profile = _profileRepository.GetAllWithChildren (p => p.MeasurementSubjectId == id, o => o.Id, null, null, null).FirstOrDefault();

			if (profile != null) {
				using(var scope = App.AutoFacContainer.BeginLifetimeScope ()) {	
					foreach (var def in profile.ProfileMeasurementDefinitions) {
						var query = scope.ResolveKeyed<IPredefinedQuery<MeasurementInstanceModel>> (def.MeasurementTypeModel.Name);
						var result = query.Query ((new List<object>() {id, def.MeasurementDefinitionId}).ToArray());
						if (result != null) {
							measurements.Add (result);
						}
					}
				}
			}

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

		private int _selectedSubjectIndex;
		public int SelectedSubjectIndex {			
			get {
				return _selectedSubjectIndex;
			}
			set {

				if (Subjects != null && Subjects.Any()) {
					Load (Subjects [value].Id);
				}

				Set (() => SelectedSubjectIndex, ref _selectedSubjectIndex, value);
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

		private ObservableCollection<MeasurementSubjectModel> _subjects;
		public ObservableCollection<MeasurementSubjectModel> Subjects {
			get{ 
				if (_subjects == null) {
					_subjects = new ObservableCollection<MeasurementSubjectModel> ();
				}

				return _subjects;
			} set { 
				_subjects = value;
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

