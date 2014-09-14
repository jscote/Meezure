using System;
using SQLite.Net.Attributes;

namespace Meezure
{
	public class MeasurementGroupUnitModel
	{
		public MeasurementGroupUnitModel ()
		{
		}

		[PrimaryKey]
		public int Id {
			get;
			set;
		}

		public int MeasurementGroupDefinitionId {
			get;
			set;
		}

		public int UnitId {
			get;
			set;
		}
	}
}

