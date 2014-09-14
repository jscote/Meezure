using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Meezure
{
	public class ProfileMeasurementDefinitionModel
	{
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

		[ManyToOne(CascadeOperations = CascadeOperation.All)]
		public MeasurementDefinitionModel Definition {
			get;
			set;
		}

		[ForeignKey(typeof(MeasurementFrequencyModel))]
		public int MeasurementFrequencyId {
			get;
			set;
		}

		[ManyToOne(CascadeOperations = CascadeOperation.All)]
		public MeasurementFrequencyModel Frequency {
			get;
			set;
		}

		[ForeignKey(typeof(ProfileModel))]
		public int ProfileId {
			get;
			set;
		}

		[ManyToOne(CascadeOperations = CascadeOperation.All)]
		public ProfileModel Profile {
			get;
			set;
		}

	}
}

