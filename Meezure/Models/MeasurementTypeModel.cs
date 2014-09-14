using System;
using SQLite.Net.Attributes;

namespace Meezure
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

