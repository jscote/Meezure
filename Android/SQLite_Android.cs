using System.IO;
using Meezure;


[assembly: Xamarin.Forms.Dependency (typeof (SQLite_Android))]
// ..

public class SQLite_Android : ISQLite {
	public SQLite_Android () {}
	public SQLite.Net.SQLiteConnection GetConnection () {
		var sqliteFilename = "MeezureSQLite.db3";
		string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
		var path = Path.Combine(documentsPath, sqliteFilename);

		//File.Delete (path);
		// Create the connection
		var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
		var conn = new SQLite.Net.SQLiteConnection(plat, path);


		// Return the database connection 
		return conn;
	}}