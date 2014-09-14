using System;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.Linq;
using SQLite.Net;
using Autofac;
using SQLiteNetExtensions.Extensions;

namespace Meezure
{
	public class App
	{

		public static IContainer AutoFacContainer;

		static App ()
		{

			var builder = new ContainerBuilder ();

			builder.RegisterType<MeasurementDashboardViewModel> ();
			builder.RegisterType<Repository<MeasurementInstanceModel>>().As<IRepository<MeasurementInstanceModel>>();
			builder.RegisterType<Repository<MeasurementDefinitionModel>>().As<IRepository<MeasurementDefinitionModel>>();
			builder.RegisterType<Repository<MeasurementSubjectModel>>().As<IRepository<MeasurementSubjectModel>>();

			builder.RegisterType<NavigationService> ();
			builder.RegisterType<DashboardPage> ();

			builder.RegisterType<UnitOfWork> ().As<IUnitOfWork> ();

			builder.RegisterType<PreferencesPage>();
			builder.RegisterType<Preference> ();

			builder.RegisterType<MeasurementPage> ();
			builder.RegisterType<MeasurementViewModel> ();

			builder.RegisterType<MeasurementListPage> ();
			builder.RegisterType<MeasurementListViewModel> ();

			builder.RegisterType<FilterPage> ();
			builder.RegisterType<FilterViewModel> ();


			AutoFacContainer = builder.Build ();

		}

		private static NavigationService _navigationService;

		public static NavigationService NavigationService
		{
			get
			{
				return _navigationService ?? (_navigationService = AutoFacContainer.Resolve<NavigationService>());
			}
		}


		private static void InitializeDatabase()
		{
			var db = DependencyService.Get<ISQLite> ().GetConnection ();

			db.CreateTable<Version> ();
			var isInstalled = false;

			/*if (db.Table<Version> ().FirstOrDefault (w => w.InstalledVersion == "0.2.3") == null) {
				db.Insert (new Version () { InstalledVersion = "0.2.3" });
			} else {
				isInstalled = true;
			}*/

			if (!isInstalled) {
				db.CreateTable<MeasurementSystemModel> ();
				if (db.Table<MeasurementSystemModel> ().Count (c => c.Id == 1) == 0) {
					db.Insert (new MeasurementSystemModel () { Id = 1, Name = "Imperial" });
				}

				if (db.Table<MeasurementSystemModel> ().Count (c => c.Id == 2) == 0) {
					db.Insert (new MeasurementSystemModel () { Id = 2, Name = "Metric" });
				}

				db.CreateTable<MeasurementUnitModel> ();

				if (db.Table<MeasurementUnitModel> ().Count (c => c.Id == 1) == 0) {
					db.Insert (new MeasurementUnitModel () {
						Id = 1,
						Abbreviation = "mmHg",
						Symbol = "mmHg",
						Name = "Millimeter of Mercury",
						MeasurementSystemId = 2
					});
				}

				if (db.Table<MeasurementUnitModel> ().Count (c => c.Id == 2) == 0) {
					db.Insert (new MeasurementUnitModel () {
						Id = 2,
						Abbreviation = "in",
						Symbol = "\"",
						Name = "Inches",
						MeasurementSystemId = 1
					});
				}

				if (db.Table<MeasurementUnitModel> ().Count (c => c.Id == 3) == 0) {
					db.Insert (new MeasurementUnitModel () {
						Id = 3,
						Abbreviation = "ft",
						Symbol = "'",
						Name = "Feet",
						MeasurementSystemId = 1
					});
				}

				if (db.Table<MeasurementUnitModel> ().Count (c => c.Id == 4) == 0) {
					db.Insert (new MeasurementUnitModel () {
						Id = 4,
						Abbreviation = "lbs",
						Symbol = "lbs",
						Name = "Pounds",
						MeasurementSystemId = 1
					});
				}

				if (db.Table<MeasurementUnitModel> ().Count (c => c.Id == 5) == 0) {
					db.Insert (new MeasurementUnitModel () {
						Id = 5,
						Abbreviation = "Bpm",
						Symbol = "bpm",
						Name = "Beat per minute",
						MeasurementSystemId = 1
					});
				}

				db.CreateTable<MeasurementSubjectModel> ();

				db.CreateTable<ProfileModel> ();

				db.CreateTable<MeasurementFrequencyModel> ();

				if (db.Table<MeasurementFrequencyModel> ().Count (c => c.Id == 1) == 0) {
					db.Insert (new MeasurementFrequencyModel () { Id = 1, Name = "Hourly" });
				}

				if (db.Table<MeasurementFrequencyModel> ().Count (c => c.Id == 2) == 0) {
					db.Insert (new MeasurementFrequencyModel () { Id = 2, Name = "Twice Daily" });
				}

				if (db.Table<MeasurementFrequencyModel> ().Count (c => c.Id == 3) == 0) {
					db.Insert (new MeasurementFrequencyModel () { Id = 3, Name = "Daily" });
				}

				if (db.Table<MeasurementFrequencyModel> ().Count (c => c.Id == 4) == 0) {
					db.Insert (new MeasurementFrequencyModel () { Id = 4, Name = "Weekly" });
				}

				if (db.Table<MeasurementFrequencyModel> ().Count (c => c.Id == 5) == 0) {
					db.Insert (new MeasurementFrequencyModel () { Id = 5, Name = "Monthly" });
				}

				db.CreateTable<ProfileMeasurementDefinitionModel> ();

				db.CreateTable<MeasurementTypeModel> ();


				if (db.Table<MeasurementTypeModel> ().Count (c => c.Id == 1) == 0) {
					db.Insert (new MeasurementTypeModel () { Id = 1, Name = "Entry" });
				}

				if (db.Table<MeasurementTypeModel> ().Count (c => c.Id == 2) == 0) {
					db.Insert (new MeasurementTypeModel () { Id = 2, Name = "Average", Abbreviation = "Avg" });
				}

				if (db.Table<MeasurementTypeModel> ().Count (c => c.Id == 3) == 0) {
					db.Insert (new MeasurementTypeModel () { Id = 3, Name = "Minimum observed", Abbreviation = "Min" });
				}

				if (db.Table<MeasurementTypeModel> ().Count (c => c.Id == 4) == 0) {
					db.Insert (new MeasurementTypeModel () { Id = 4, Name = "Maximum observed", Abbreviation = "Max" });
				}

				if (db.Table<MeasurementTypeModel> ().Count (c => c.Id == 5) == 0) {
					db.Insert (new MeasurementTypeModel () { Id = 5, Name = "Minimum Goal", Abbreviation = "Min Goal" });
				}

				if (db.Table<MeasurementTypeModel> ().Count (c => c.Id == 6) == 0) {
					db.Insert (new MeasurementTypeModel () { Id = 6, Name = "Maximum Goal", Abbreviation = "Max Goal" });
				}

				db.CreateTable<MeasurementCategoryModel> ();

				if (db.Table<MeasurementCategoryModel> ().Count (c => c.Id == 1) == 0) {
					db.Insert (new MeasurementCategoryModel () { Id = 1, Name = "Weight" });
				}

				if (db.Table<MeasurementCategoryModel> ().Count (c => c.Id == 2) == 0) {
					db.Insert (new MeasurementCategoryModel () { Id = 2, Name = "Length" });
				}

				if (db.Table<MeasurementCategoryModel> ().Count (c => c.Id == 3) == 0) {
					db.Insert (new MeasurementCategoryModel () { Id = 3, Name = "Biology" });
				}

				db.CreateTable<MeasurementDefinitionModel> ();
				if (db.Table<MeasurementDefinitionModel> ().Count (c => c.Id == 1) == 0) {
					db.Insert (new MeasurementDefinitionModel () { Id = 1, Name = "Blood Pressure" });
				}

				if (db.Table<MeasurementDefinitionModel> ().Count (c => c.Id == 2) == 0) {
					db.Insert (new MeasurementDefinitionModel () { Id = 2, Name = "Weight" });
				}

				if (db.Table<MeasurementDefinitionModel> ().Count (c => c.Id == 3) == 0) {
					db.Insert (new MeasurementDefinitionModel () { Id = 3, Name = "Heart Rate" });
				}

				db.CreateTable<MeasurementGroupDefinitionModel> ();
				if (db.Table<MeasurementGroupDefinitionModel> ().Count (c => c.Id == 1) == 0) {
					db.Insert (new MeasurementGroupDefinitionModel () {
						Id = 1,
						Name = "Systolic",
						MeasurementDefinitionId = 1,
						MeasurementCategoryId = 2,
						DefaultUnitId = 1
					});
				}

				if (db.Table<MeasurementGroupDefinitionModel> ().Count (c => c.Id == 2) == 0) {
					db.Insert (new MeasurementGroupDefinitionModel () {
						Id = 2,
						Name = "Diastolic",
						MeasurementDefinitionId = 1,
						MeasurementCategoryId = 2,
						DefaultUnitId = 1
					});
				}

				if (db.Table<MeasurementGroupDefinitionModel> ().Count (c => c.Id == 3) == 0) {
					db.Insert (new MeasurementGroupDefinitionModel () {
						Id = 3,
						Name = "Weight",
						MeasurementDefinitionId = 2,
						MeasurementCategoryId = 2,
						DefaultUnitId = 4
					});
				}

				if (db.Table<MeasurementGroupDefinitionModel> ().Count (c => c.Id == 4) == 0) {
					db.Insert (new MeasurementGroupDefinitionModel () {
						Id = 4,
						Name = "Heart Rate",
						MeasurementDefinitionId = 3,
						MeasurementCategoryId = 2,
						DefaultUnitId = 5
					});
				}

				db.CreateTable<MeasurementGroupUnitModel> ();
				if (db.Table<MeasurementGroupUnitModel> ().Count (c => c.Id == 1) == 0) {
					db.Insert (new MeasurementGroupUnitModel () {
						Id = 1,
						MeasurementGroupDefinitionId = 1,
						UnitId = 1
					});
				}

				if (db.Table<MeasurementGroupUnitModel> ().Count (c => c.Id == 2) == 0) {
					db.Insert (new MeasurementGroupUnitModel () {
						Id = 2,
						MeasurementGroupDefinitionId = 2,
						UnitId = 1
					});
				}

				if (db.Table<MeasurementGroupUnitModel> ().Count (c => c.Id == 3) == 0) {
					db.Insert (new MeasurementGroupUnitModel () {
						Id = 3,
						MeasurementGroupDefinitionId = 3,
						UnitId = 4
					});
				}

				if (db.Table<MeasurementGroupUnitModel> ().Count (c => c.Id == 4) == 0) {
					db.Insert (new MeasurementGroupUnitModel () {
						Id = 4,
						MeasurementGroupDefinitionId = 4,
						UnitId = 5
					});
				}

				db.CreateTable<MeasurementInstanceModel> ();
				db.CreateTable<MeasurementGroupInstanceModel> ();

				CreateTestData (db);
			}

		}

		static void CreateTestData (SQLiteConnection db)
		{
			var js = new MeasurementSubjectModel () { Name = "JS"};
			if (db.Table<MeasurementSubjectModel> ().Count (c => c.Name == "JS") == 0) {
				db.Insert (js);
			} else {
				js = db.Table<MeasurementSubjectModel> ().Where (w => w.Name == "JS").FirstOrDefault ();
			}
				
			var jsProfile = new ProfileModel () { MeasurementSubjectId = js.Id};

			var jp = db.Query<ProfileModel> ("SELECT P.* FROM ProfileModel as P JOIN MeasurementSubjectModel AS s ON p.MeasurementSubjectId == s.Id WHERE s.Name == ?", "JS");
			if(jp.Count == 0) {
				db.Insert (jsProfile);

				var v = new ProfileMeasurementDefinitionModel () {
					MeasurementFrequencyId = 3,
					MeasurementDefinitionId = 1,
					ProfileId = jsProfile.Id
				};

				var c = db.Insert (v);

				db.Insert(new ProfileMeasurementDefinitionModel () {
					MeasurementFrequencyId = 4 ,
					MeasurementDefinitionId = 2 ,
					ProfileId = jsProfile.Id
				});
				db.Insert (new ProfileMeasurementDefinitionModel () {
					MeasurementFrequencyId = 3 ,
					MeasurementDefinitionId = 3 ,
					ProfileId = jsProfile.Id

				});


			} else {
				jsProfile = jp.FirstOrDefault ();
				db.GetChildren (jsProfile, true);
			}

			var stella = new MeasurementSubjectModel () { Name = "Stella" };
			if (db.Table<MeasurementSubjectModel> ().Count (c => c.Name == "Stella") == 0) {
				db.Insert (stella);
			}else {
				stella = db.Table<MeasurementSubjectModel> ().Where (w => w.Name == "Stella").FirstOrDefault ();
			}

			var stellaProfile = new ProfileModel () { MeasurementSubjectId = stella.Id };

			var sp = db.Query<ProfileModel> ("SELECT P.* FROM ProfileModel as P JOIN MeasurementSubjectModel AS s ON p.MeasurementSubjectId == s.Id WHERE s.Name == ?", "Stella");
			if(sp.Count == 0) {
				db.Insert (stellaProfile);

				var v = new ProfileMeasurementDefinitionModel () {
					MeasurementFrequencyId = 3,
					MeasurementDefinitionId = 1,
					ProfileId = stellaProfile.Id
				};

				var c = db.Insert (v);

				db.Insert(new ProfileMeasurementDefinitionModel () {
					MeasurementFrequencyId = 4 ,
					MeasurementDefinitionId = 2 ,
					ProfileId = stellaProfile.Id
				});
				db.Insert (new ProfileMeasurementDefinitionModel () {
					MeasurementFrequencyId = 3 ,
					MeasurementDefinitionId = 3 ,
					ProfileId = stellaProfile.Id

				});

			} else {
				stellaProfile = sp.FirstOrDefault ();
				db.GetChildren (stellaProfile, true);
			}


			var isabelle = new MeasurementSubjectModel () { Name = "Isabelle" };
			if (db.Table<MeasurementSubjectModel> ().Count (c => c.Name == "Isabelle") == 0) {
				db.Insert (isabelle);
			}else {
				isabelle = db.Table<MeasurementSubjectModel> ().Where (w => w.Name == "Isabelle").FirstOrDefault ();
			}

			var jsBloodPressure = new MeasurementInstanceModel () {DateRecorded = DateTime.Now, MeasurementSubjectId = js.Id, MeasurementDefinitionId = 1 };
			var jsBloodPressure2 = new MeasurementInstanceModel () {DateRecorded = DateTime.Now.AddDays(-1), MeasurementSubjectId = js.Id, MeasurementDefinitionId = 1 };
			var jsBloodPressure3 = new MeasurementInstanceModel () {DateRecorded = DateTime.Now.AddDays(-2), MeasurementSubjectId = js.Id, MeasurementDefinitionId = 1 };

			if (db.Table<MeasurementInstanceModel> ().Count (w => w.MeasurementSubjectId == js.Id && w.MeasurementDefinitionId == 1) == 0) {
				db.Insert (jsBloodPressure);
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = jsBloodPressure.Id, MeasurementGroupDefinitionId = 1, UnitId = 1, Value = 120 });
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = jsBloodPressure.Id, MeasurementGroupDefinitionId = 2, UnitId = 1, Value = 80 });

				db.Insert (jsBloodPressure2);
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = jsBloodPressure2.Id, MeasurementGroupDefinitionId = 1, UnitId = 1, Value = 123 });
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = jsBloodPressure2.Id, MeasurementGroupDefinitionId = 2, UnitId = 1, Value = 84 });

				db.Insert (jsBloodPressure3);
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = jsBloodPressure3.Id, MeasurementGroupDefinitionId = 1, UnitId = 1, Value = 117 });
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = jsBloodPressure3.Id, MeasurementGroupDefinitionId = 2, UnitId = 1, Value = 78 });
			}

			var jsBpm = new MeasurementInstanceModel () {DateRecorded = DateTime.Now, MeasurementSubjectId = js.Id, MeasurementDefinitionId = 3 };
			var jsBpm2 = new MeasurementInstanceModel () {DateRecorded = DateTime.Now.AddDays(-1), MeasurementSubjectId = js.Id, MeasurementDefinitionId = 3 };
			var jsBpm3 = new MeasurementInstanceModel () {DateRecorded = DateTime.Now.AddDays(-2), MeasurementSubjectId = js.Id, MeasurementDefinitionId = 3 };

			if (db.Table<MeasurementInstanceModel> ().Count (w => w.MeasurementSubjectId == js.Id && w.MeasurementDefinitionId == 3) == 0) {
				db.Insert (jsBpm);
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = jsBpm.Id, MeasurementGroupDefinitionId = 4, UnitId = 5, Value = 72 });

				db.Insert (jsBpm2);
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = jsBpm2.Id, MeasurementGroupDefinitionId = 4, UnitId = 5, Value = 80 });

				db.Insert (jsBpm3);
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = jsBpm3.Id, MeasurementGroupDefinitionId = 4, UnitId = 5, Value = 69 });
			}

			var stellaBloodPressure = new MeasurementInstanceModel () {DateRecorded = DateTime.Now, MeasurementSubjectId = stella.Id, MeasurementDefinitionId = 1 };
			var stellaBloodPressure2 = new MeasurementInstanceModel () {DateRecorded = DateTime.Now.AddDays(-1), MeasurementSubjectId = stella.Id, MeasurementDefinitionId = 1 };
			var stellaBloodPressure3 = new MeasurementInstanceModel () {DateRecorded = DateTime.Now.AddDays(-2), MeasurementSubjectId = stella.Id, MeasurementDefinitionId = 1 };

			if (db.Table<MeasurementInstanceModel> ().Count (w => w.MeasurementSubjectId == stella.Id && w.MeasurementDefinitionId == 1) == 0) {
				db.Insert (stellaBloodPressure);
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = stellaBloodPressure.Id, MeasurementGroupDefinitionId = 1, UnitId = 1, Value = 120 });
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = stellaBloodPressure.Id, MeasurementGroupDefinitionId = 2, UnitId = 1, Value = 80 });

				db.Insert (stellaBloodPressure2);
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = stellaBloodPressure2.Id, MeasurementGroupDefinitionId = 1, UnitId = 1, Value = 123 });
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = stellaBloodPressure2.Id, MeasurementGroupDefinitionId = 2, UnitId = 1, Value = 84 });

				db.Insert (stellaBloodPressure3);
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = stellaBloodPressure3.Id, MeasurementGroupDefinitionId = 1, UnitId = 1, Value = 117 });
				db.Insert (new MeasurementGroupInstanceModel () { MeasurementInstanceId = stellaBloodPressure3.Id, MeasurementGroupDefinitionId = 2, UnitId = 1, Value = 78 });
			}
		
		}

		public static Page GetMainPage ()
		{	

			InitializeDatabase ();

			var mainNav = new MainMenu ();
			NavigationService.Navigation = mainNav.Detail.Navigation;

			////var mainNav = new NavigationPage (_container.Resolve<HomePage>("HomePage"));

			////NavigationService.Navigation = mainNav.Navigation;

			var tabPage = new TabbedPage ();
			tabPage.Children.Add (mainNav);

			//var secondPage = new AllMeasurements ();
			//tabPage.Children.Add (secondPage);


			return tabPage;

			/*return new ContentPage { 
				Content = new StackLayout {
					Children = {
						new Label {Text = "Enter a measurement for Something"},
						new Entry {Placeholder = "Enter a value in lbs"},
						new Entry {Placeholder = "Enter a value in oz"}
					},
					VerticalOptions = LayoutOptions.CenterAndExpand,
					HorizontalOptions = LayoutOptions.CenterAndExpand,
				},
			};*/
		}
	}
}

