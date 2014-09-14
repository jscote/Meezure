using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace Meezure
{

	public class MeasurementSubjectListViewModel : ViewModelBase
	{

		public ObservableCollection<MeasurementSubjectViewModel> Measurements {
			get;
			set;
		}

		public MeasurementSubjectListViewModel()
		{
			Measurements = new ObservableCollection<MeasurementSubjectViewModel> ();
		}

	}

	public class MeasurementSubjectViewModel : ViewModelBase
	{
		public MeasurementSubjectViewModel ()
		{
			this.SaveMeasurementSubject = new Command (() => DateTimeCaptured = DateTimeOffset.Now);
		}

		public MeasurementSubjectViewModel(string name, string description, MeasurementSubjectViewModel parent) : this()
		{
			Name = name;
			Description = description;
			ParentSubject = parent;
		}

		private int _id;
		public int Id {
			get {
				return _id;
			}
			set {
				Set (() => Id, ref _id, value);

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

		private string _description;
		public string Description {
			get {
				return _description;
			}
			set {
				Set (() => Description, ref _description, value);

			}
		}

		private MeasurementSubjectViewModel _parentSubject;
		public MeasurementSubjectViewModel ParentSubject {
			get {
				return _parentSubject;
			}
			set {
				Set (() => ParentSubject, ref _parentSubject, value);

			}
		}


		private DateTimeOffset _dateTimeCaptured;
		public DateTimeOffset DateTimeCaptured {
			get {
				return _dateTimeCaptured;
			}
			set {
				Set (() => DateTimeCaptured, ref _dateTimeCaptured, value);

			}
		}

		public ICommand SaveMeasurementSubject { protected set; get; }
	}
}

