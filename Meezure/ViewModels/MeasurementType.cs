using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Meezure
{

	public class MeasurementUnitList : ViewModelBase {

		public ObservableCollection<MeasurementUnit> MeasurementUnits {
			get;
			set;
		}

		public MeasurementUnitList ()
		{
			MeasurementUnits = new ObservableCollection<MeasurementUnit> ();
			MeasurementUnits.Add (new MeasurementUnit (MeasurementSystem.Metric, "Centimeter", "cm"));
			MeasurementUnits.Add (new MeasurementUnit (MeasurementSystem.Imperial, "Pounds", "lbs"));

			this.AddUnit = new Command (() => {MeasurementUnits.Add (new MeasurementUnit(MeasurementSystem.Metric, null, null));});
			this.SaveAllUnits = new Command (() => {
				foreach (var unit in MeasurementUnits) {
					unit.SaveUnit.Execute (unit);
				}
			});

		}

		public ICommand AddUnit { protected set; get; } 
		public ICommand SaveAllUnits { protected set; get; }
	}

	public class MeasurementUnit : ViewModelBase
	{
		public MeasurementUnit (MeasurementSystem msys, string unit, string abbr)
		{

			this.MeasurementSystem = msys;
			this.UnitName = unit;
			this.UnitAbbreviation = abbr;

			this.SaveUnit = new Command<MeasurementUnit> ((uom) => {
				//Save the preferences to the model.
				if(this.Id == 0) {
					//we need to insert
				}
				else {
					//we need to update
				}
			});

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

		private MeasurementSystem _measurementSystem = MeasurementSystem.Metric;
		public MeasurementSystem MeasurementSystem {
			get {
				return _measurementSystem;
			}
			set {
				Set (() => MeasurementSystem, ref _measurementSystem, value);

			}
		}

		private string _unitName;
		public string UnitName {
			get {
				return _unitName;
			}
			set {
				Set (() => UnitName, ref _unitName, value);

			}
		}

		private string _unitAbbreviation;
		public string UnitAbbreviation {
			get {
				return _unitAbbreviation;
			}
			set {
				Set (() => UnitAbbreviation, ref _unitAbbreviation, value);

			}
		}

		public ICommand SaveUnit { internal set; get; }
	}
}

