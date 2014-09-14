using System;
using Autofac;

namespace Meezure
{
	public abstract class BasePredefinedQuery<TEntity> : IPredefinedQuery<TEntity> where TEntity : class
	{
		private ILifetimeScope _scope;
		private IUnitOfWork _unitOfWork { get; set;}

		public BasePredefinedQuery ()
		{
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

		public abstract TEntity Query (object[] parameters);
	}

}

