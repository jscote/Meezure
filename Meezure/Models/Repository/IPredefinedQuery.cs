using System;

namespace Meezure
{
	public interface IPredefinedQuery<TEntity> where TEntity : class
	{

		TEntity Query(object[] parameters);
		IUnitOfWork Session { get;}
	}
}

