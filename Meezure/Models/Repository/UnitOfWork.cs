using System;
using Xamarin.Forms;
using SQLite.Net;

namespace Meezure
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{

		private SQLiteConnection _connection;

		public SQLiteConnection Connection { 
			get {

				if (_connection == null) {
					_connection = DependencyService.Get<ISQLite> ().GetConnection ();
				}


				return _connection;	
			}
		}

		public UnitOfWork ()
		{

		}

		public void Dispose() {
			if (_connection != null) {
				if (!_connection.IsInTransaction) {
					_connection.Rollback ();
				}
				_connection.Dispose ();
			}
		}

		public void StartTransaction() {
			if (!_connection.IsInTransaction) {
				_connection.BeginTransaction ();
			}
		}

		public void Save() {
			if (_connection.IsInTransaction) {
				_connection.Commit ();
			}
		}

		public void Cancel() {
			if (_connection.IsInTransaction) {
				_connection.Rollback ();
			}
		}
	}
}

