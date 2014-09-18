using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace Meezure
{
	public class MeasurementViewModel : ViewModelBase
	{
		private IRepository<MeasurementDefinitionModel> _definitionRepository;
		private IRepository<MeasurementInstanceModel> _instanceRepository;
		private IRepository<MeasurementSubjectModel> _subjectRepository;

		private string _loadingMsgId = string.Format ("Loading:{0}", MeasurementPage.PageName.Remove (MeasurementPage.PageName.IndexOf ("Page")));
		private int _currentMode = 0;

		public MeasurementViewModel (IRepository<MeasurementDefinitionModel> defRepository, 
			IRepository<MeasurementInstanceModel> instanceRepository, IRepository<MeasurementSubjectModel> subjectRepository){
			_definitionRepository = defRepository;
			_instanceRepository = instanceRepository;
			_subjectRepository = subjectRepository;
			MessengerInstance.Register<NotificationMessage<IDictionary<string, int>>> (this, _loadingMsgId, (msg) => Load (msg.Content));
			this.SaveMeasurement = new Command (() => Save());
		}

		private void Load(IDictionary<string, int> parameters)
		{
			var subjectId = 0;
			var definitionId = 0;
	

			parameters.TryGetValue ("SubjectId", out subjectId);
			parameters.TryGetValue ("DefinitionId", out definitionId);
			parameters.TryGetValue ("Mode", out _currentMode);

			MeasurementSubjectId = subjectId;

			if (definitionId == 0) {
				var measurement = _instanceRepository.GetAll (predicate: null, orderBy: (o) => o.DateRecorded, descending: true, skip: 0, count: 1).FirstOrDefault ();

				if (measurement != null) {
					LoadFromDefinition (measurement.MeasurementDefinitionId, measurement.MeasurementSubjectId);
				}
			} else {
				LoadFromDefinition (definitionId, subjectId);
			}

			UnregisterMessages ();

		}

		private void LoadFromDefinition(int definitionId, int subjectId)
		{

			var definition = _definitionRepository.GetAllWithChildren (predicate: (w) => w.Id == definitionId, orderBy: (o) => o.Id, descending: false, skip: null, count: null).FirstOrDefault ();

			//get the subject
			var subjectName = _subjectRepository.GetAll (w => w.Id == subjectId).Select (s => s.Name).FirstOrDefault ();
			

			DateTimeCaptured = DateTime.Now;
			MeasurementGroup = new MeasurementItemGroupViewModel () {
				DefinitionId = definition.Id,
				DefinitionName = definition.Name,
				SubjectId = subjectId,
				SubjectName = subjectName,
				MeasurementType = "MeasurementEntry",
				MeasurementItems = new ObservableCollection<MeasurementItemViewModel> ()
			};
			foreach (var group in definition.MeasurementGroupDefinitions) {
				MeasurementGroup.MeasurementItems.Add (new MeasurementItemViewModel () { 
					DefinitionId = group.Id,
					Name = group.Name,
					Uom = group.DefaultUnit.Abbreviation,
					UomId = group.DefaultUnitId,
					Value = 0
				});

			}
		}

		private MeasurementItemGroupViewModel _measurementGroup;
		public MeasurementItemGroupViewModel MeasurementGroup {
			get {
				return _measurementGroup;
			}
			set {
				Set (() => MeasurementGroup, ref _measurementGroup, value);

			}
		}


		private int _measurementSubjectId;
		public int MeasurementSubjectId {
			get {
				return _measurementSubjectId;
			}
			set {
				Set (() => MeasurementSubjectId, ref _measurementSubjectId, value);

			}
		}

		private DateTime _dateTimeCaptured;
		public DateTime DateTimeCaptured {
			get {
				return _dateTimeCaptured;
			}
			set {
				Set (() => DateTimeCaptured, ref _dateTimeCaptured, value);

			}
		}


		private void Save() {
			DateTimeCaptured = DateTime.Now;
			UnregisterMessages ();

			//We are adding a new measurement - this may not be how we will do it in the end...We may just load the instanceId, and if it is 0 then we insert, otherwise update.
			if (_currentMode == 0) {
				InsertMeasurement ();
			} else if (_currentMode == 1) {
				//We will be updating an existing measurement
			}

			Messenger.Default.Send<NotificationMessage<int>> (new NotificationMessage<int>(MeasurementSubjectId, "Loading:Dashboard"), "Loading:Dashboard");
			App.NavigationService.NavigateBack ();
		}

		private void InsertMeasurement() {
			var measurement = new MeasurementInstanceModel ();
			measurement.DateRecorded = DateTimeCaptured;
			measurement.Definition = new MeasurementDefinitionModel () { Id = MeasurementGroup.DefinitionId };
			measurement.Subject = new MeasurementSubjectModel () {Id = MeasurementGroup.SubjectId};
			measurement.MeasurementGroups = new List<MeasurementGroupInstanceModel>();
			foreach(var measure in MeasurementGroup.MeasurementItems)
			{
				measurement.MeasurementGroups.Add (new MeasurementGroupInstanceModel () {
					MeasurementGroupDefinitionModel = new MeasurementGroupDefinitionModel () { Id = measure.DefinitionId },
					Value = measure.Value,
					Unit = new MeasurementUnitModel () { Id = measure.UomId },
				});
			}

			_instanceRepository.Add(measurement);
			_instanceRepository.Session.Save ();
			Messenger.Default.Send<NotificationMessage> (new NotificationMessage("Loading:Dashboard"), "Loading:Dashboard");


		}

		private void UnregisterMessages() {
			MessengerInstance.Unregister<NotificationMessage<IDictionary<string, int>>> (this, _loadingMsgId);
		}

		public ICommand SaveMeasurement { protected set; get; }
	}
}

