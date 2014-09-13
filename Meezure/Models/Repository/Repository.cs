using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using System.Linq.Expressions;
using SQLiteNetExtensions.Extensions;

namespace MeasureONE
{
	public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity: class, new()
	{
		private ILifetimeScope _scope;
		private IUnitOfWork _unitOfWork { get; set;}

		#region IRepository implementation

		public IList<TEntity> GetAll ()
		{
			return Session.Connection.Table<TEntity> ().ToList();
		}

		public IList<TEntity> GetAll (System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
		{
			return Session.Connection.Table<TEntity> ().Where(predicate).ToList();
		}

		public IList<TEntity> GetAll <TValue>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TValue>> orderBy, bool? descending, int? skip, int? count)
		{

			var query = Session.Connection.Table<TEntity> ();
			if (predicate != null) {
				query = query.Where (predicate);
			}
			if (orderBy != null) {
				if (descending.HasValue && descending.Value) {
					query = query.OrderByDescending (orderBy);
				} else {
					query = query.OrderBy (orderBy);
				}
			}
			if (skip.HasValue) {
				query = query.Skip (skip.Value);
			}
			if (count.HasValue) {
				query = query.Take (count.Value);
			}

			return query.ToList();
		}

		public IList<TEntity> GetAllWithChildren <TValue>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TValue>> orderBy, bool? descending, int? skip, int? count)
		{

			var list = this.GetAll (predicate, orderBy, descending, skip, count);

			foreach (TEntity entry in list) {
				Session.Connection.GetChildren (entry, true);
			}

			return list;
		}

		public IList<TEntity> GetAllWithChildren(string query, object[] parameters ) {

			var list = Session.Connection.Query<TEntity> (query, parameters);

			foreach (TEntity entry in list) {
				Session.Connection.GetChildren (entry, true);
			}

			return list;
		}


		public bool Add (TEntity entity)
		{
			if (IsValid (entity)) {

				Session.StartTransaction ();
			
				Session.Connection.InsertWithChildren (entity, true);

				return true;
			}
			return false;
		}

		public bool Delete (int primaryKey)
		{
			var rowCount = Session.Connection.Delete<TEntity> (primaryKey);
			return rowCount > 0;
		}

		public bool Update (TEntity entity)
		{
			var rowCount = Session.Connection.Update(entity);
			return rowCount > 0;
		}

		public bool IsValid (TEntity entity)
		{
			return true;
		}

		public IUnitOfWork Session {
			get {

				if (_unitOfWork == null) {
					_scope = App.AutoFacContainer.BeginLifetimeScope ();
					_unitOfWork = _scope.Resolve<IUnitOfWork> ();
				}

				return _unitOfWork;
			}
			set {
				_unitOfWork = value;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{

			if (_scope != null)
				_scope.Dispose ();

			if (_unitOfWork != null)
				_unitOfWork.Dispose ();
		}

		#endregion
	}
}

