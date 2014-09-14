using System;
using SQLite.Net;

namespace Meezure
{
	public interface IUnitOfWork : IDisposable
	{
		void StartTransaction();
		void Save();
		void Cancel();
		SQLiteConnection Connection {get;}
	}
}

