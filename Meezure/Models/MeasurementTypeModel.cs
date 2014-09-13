using System;
using SQLite.Net.Attributes;

namespace MeasureONE
{
	public class MeasurementTypeModel
	{
		public MeasurementTypeModel ()
		{
		}

		[PrimaryKey]
		public int Id {
			get;
			set;
		}
		public string Name {
			get;
			set;
		}
		public string Abbreviation {
			get;
			set;
		}
	}
}

