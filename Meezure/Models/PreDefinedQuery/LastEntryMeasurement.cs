using System;
using System.Linq;

namespace Meezure
{
	public class LastEntryMeasurement : BasePredefinedQuery<MeasurementInstanceModel>
	{
		private IRepository<MeasurementInstanceModel> _repository;

		#region IPredefinedQuery implementation

		public override MeasurementInstanceModel Query (object[] parameters)
		{
			string selectStr = "SELECT t1.* FROM MeasurementInstanceModel AS t1 WHERE t1.MeasurementSubjectId == ? AND t1.DateRecorded ==  (SELECT MAX(t2.DateRecorded)  FROM MeasurementInstanceModel AS t2 WHERE t2.MeasurementSubjectId == t1.MeasurementSubjectId AND t2.MeasurementDefinitionId == t1.MeasurementDefinitionId limit 1) AND t1.MeasurementDefinitionId == ?";
		
			var data = _repository.GetAllWithChildren (selectStr, parameters);

			return data.FirstOrDefault();

		}

		#endregion


		public LastEntryMeasurement (IRepository<MeasurementInstanceModel> repository)
		{
			_repository = repository;
		}

	}
}

