using System;
using SQLite.Net.Attributes;

namespace MeasureONE
{
	public class MeasurementSystemModel
	{
		public MeasurementSystemModel ()
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
	}
}

