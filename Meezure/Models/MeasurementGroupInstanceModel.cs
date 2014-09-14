using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Meezure
{
	public class MeasurementGroupInstanceModel
	{
		public MeasurementGroupInstanceModel ()
		{
		}

		[PrimaryKey]
		[AutoIncrement]
		public int Id {
			get;
			set;
		}

		[ForeignKey(typeof(MeasurementGroupDefinitionModel))]
		public int MeasurementGroupDefinitionId {
			get;
			set;
		}

		[ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
		public MeasurementGroupDefinitionModel MeasurementGroupDefinitionModel {
			get;
			set;
		}

		[ForeignKey(typeof(MeasurementInstanceModel))]
		public int MeasurementInstanceId {
			get;
			set;
		}

		[ManyToOne]
		public MeasurementInstanceModel MeasurementInstance {
			get;
			set;
		}

		public decimal Value {
			get;
			set;
		}

		[ForeignKey(typeof(MeasurementUnitModel))]
		public int UnitId {
			get;
			set;
		}

		[ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
		public MeasurementUnitModel Unit {
			get;
			set;
		}

	}
}

