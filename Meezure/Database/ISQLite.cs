using System;
using SQLite.Net;

namespace MeasureONE
{
	public interface ISQLite {
		SQLiteConnection GetConnection();
	}
}

