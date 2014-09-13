using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace MeasureONE
{
	public class MeasurementDefinitionModel
	{

		[PrimaryKey]
		public int Id {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<MeasurementGroupDefinitionModel> MeasurementGroupDefinitions {
			get;
			set;
		}


	}
}

