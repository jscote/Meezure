using System;
using SQLite.Net.Attributes;

namespace MeasureONE
{
	public class MeasurementUnitModel
	{

		[PrimaryKey]
		public int Id {
			get;
			set;
		}

		public string Abbreviation {
			get;
			set;
		}

		public string Symbol {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}
			
		public int MeasurementSystemId {
			get;
			set;
		}

	}
}

