using System;
using System.Linq;

namespace Meezure
{
	public class AvgMeasurement : BasePredefinedQuery<MeasurementInstanceModel> 
	{
		class GroupValue {
			public int MeasurementGroupDefinitionId { get; set;}
			public decimal Value { get; set;}
		}

		private IRepository<MeasurementInstanceModel> _repository;
		private IRepository<MeasurementGroupInstanceModel> _groupRepository;

		#region IPredefinedQuery implementation

		public override MeasurementInstanceModel Query (object[] parameters)
		{
			string selectStr = "SELECT t1.MeasurementGroupDefinitionId, Avg(t1.Value) AS Value FROM MeasurementGroupInstanceModel AS t1 JOIN MeasurementInstanceModel AS t2 ON t1.MeasurementInstanceId == t2.Id  WHERE t2.MeasurementSubjectId == ? AND t2.MeasurementDefinitionId == ? GROUP BY t1.MeasurementGroupDefinitionId";

			var intermediary = _groupRepository.Session.Connection.Query<GroupValue> (selectStr, parameters);

			var subjectId = (int)parameters [0];
			var definitionId = (int)parameters [1];

			var data = _repository.GetAllWithChildren (p => p.MeasurementSubjectId == subjectId  && p.MeasurementDefinitionId == definitionId,
				o => o.DateRecorded, true, 0, 1).FirstOrDefault();

			if (data != null) {
				foreach (var g in data.MeasurementGroups) {
					g.Value = intermediary
						.FirstOrDefault (w => w.MeasurementGroupDefinitionId == g.MeasurementGroupDefinitionId)
						.Value;
				}
			}

			return data;


		}

		#endregion

		public AvgMeasurement (IRepository<MeasurementInstanceModel> repository, IRepository<MeasurementGroupInstanceModel> groupRepository)
		{
			_repository = repository;
			_groupRepository = groupRepository;

		}


	}
}

