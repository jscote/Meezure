using System;
using SQLite.Net.Attributes;

namespace Meezure
{
	public class MeasurementCategoryModel
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
	}


}

