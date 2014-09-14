using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Meezure
{
	public class ProfileModel
	{
		public ProfileModel ()
		{
		}

		[PrimaryKey]
		[AutoIncrement]
		public int Id {
			get;
			set;
		}

		[ForeignKey(typeof(MeasurementSubjectModel))]
		public int MeasurementSubjectId { get; set; }

		[ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
		public MeasurementSubjectModel Subject {
			get;
			set;
		}

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<ProfileMeasurementDefinitionModel> ProfileMeasurementDefinitions { get; set; }


	}
}

