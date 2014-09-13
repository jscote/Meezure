using System;
using SQLite.Net.Attributes;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace MeasureONE
{
	public class MeasurementInstanceModel
	{
		public MeasurementInstanceModel ()
		{
		}

		[PrimaryKey]
		[AutoIncrement]
		public int Id {
			get;
			set;
		}

		[ForeignKey(typeof(MeasurementDefinitionModel))]
		public int MeasurementDefinitionId {
			get;
			set;
		}

		[ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
		public MeasurementDefinitionModel Definition {
			get;
			set;
		}

		[ForeignKey(typeof(MeasurementSubjectModel))]
		public int MeasurementSubjectId {
			get;
			set;
		}

		[ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
		public MeasurementSubjectModel Subject {
			get;
			set;
		}

		public DateTime DateRecorded {
			get;
			set;
		}

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<MeasurementGroupInstanceModel> MeasurementGroups {
			get;
			set;
		}
	}
}

