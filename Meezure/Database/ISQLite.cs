using System;
using SQLite.Net;

namespace Meezure
{
	public interface ISQLite {
		SQLiteConnection GetConnection();
	}
}

