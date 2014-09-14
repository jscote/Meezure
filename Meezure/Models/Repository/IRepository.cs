using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Meezure
{
	public interface IRepository<TEntity> : IDisposable where TEntity : class
	{
		IUnitOfWork Session { get;}

		IList<TEntity> GetAll();
		IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
		IList<TEntity> GetAll <TValue> (Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TValue>> orderBy, bool? descending, int? skip, int? count);
		IList<TEntity> GetAllWithChildren <TValue> (Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TValue>> orderBy, bool? descending, int? skip, int? count);
		IList<TEntity> GetAllWithChildren (string query, object[] parameters);
   	
		bool Add(TEntity entity);
		bool Delete(int primaryKey);
		bool Update(TEntity entity);
		bool IsValid(TEntity entity);
	}
}

