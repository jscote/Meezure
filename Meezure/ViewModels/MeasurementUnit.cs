using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace MeasureONE
{

	public class MeasurementTypeList : ViewModelBase {

		public ObservableCollection<MeasurementType> MeasurementTypes {
			get;
			set;
		}

		public MeasurementTypeList ()
		{
			MeasurementTypes = new ObservableCollection<MeasurementType> ();
			MeasurementTypes.Add (new MeasurementType ("Weight"));
			MeasurementTypes.Add (new MeasurementType ("2D Dimension"));
			MeasurementTypes.Add (new MeasurementType ("3D Dimension"));

			this.AddType = new Command (() => {MeasurementTypes.Add (new MeasurementType(null));});
			this.SaveAllTypes = new Command (() => {
				foreach (var t in MeasurementTypes) {
					t.SaveType.Execute (t);
				}
			});

		}

		public ICommand AddType { protected set; get; } 
		public ICommand SaveAllTypes { protected set; get; }
	}

	public class MeasurementType : ViewModelBase
	{
		public MeasurementType (string name)
		{

			this.TypeName = name;

			this.SaveType = new Command<MeasurementType> ((uom) => {
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

		private string _typeName;
		public string TypeName {
			get {
				return _typeName;
			}
			set {
				Set (() => TypeName, ref _typeName, value);

			}
		}

		public ICommand SaveType { internal set; get; }
	}
}

