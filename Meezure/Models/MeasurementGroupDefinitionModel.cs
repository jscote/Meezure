using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace MeasureONE
{
	public class MeasurementGroupDefinitionModel
	{
		public MeasurementGroupDefinitionModel ()
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

		[ForeignKey(typeof(MeasurementDefinitionModel))]
		public int MeasurementDefinitionId {
			get;
			set;
		}

		[ManyToOne]
		public MeasurementDefinitionModel MeasurementDefinition {
			get;
			set;
		}

		public int MeasurementCategoryId { 
			get; 
			set; 
		}

		[ForeignKey(typeof(MeasurementUnitModel))]
		public int DefaultUnitId {
			get;
			set;
		}

		[ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
		public MeasurementUnitModel DefaultUnit {
			get;
			set;
		}

		[OneToOne]
		public List<MeasurementGroupInstanceModel> MeasurementGroupInstances { get; set; }
	}
}

