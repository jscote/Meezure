using System;
using SQLite.Net.Attributes;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace MeasureONE
{
	public class MeasurementSubjectModel
	{
		public MeasurementSubjectModel ()
		{
		}

		[PrimaryKey]
		[AutoIncrement]
		public int Id {
			get;
			set;
		}
		public string Name {
			get;
			set;
		}

		[OneToMany (CascadeOperations = CascadeOperation.All)]
		public List<MeasurementInstanceModel> MeasurementInstances {get;set;}
	}
}

