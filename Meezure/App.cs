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
			builder.RegisterType<Repository<MeasurementGroupInstanceModel>>().As<IRepository<MeasurementGroupInstanceModel>>();
			builder.RegisterType<Repository<MeasurementDefinitionModel>>().As<IRepository<MeasurementDefinitionModel>>();
			builder.RegisterType<Repository<MeasurementSubjectModel>>().As<IRepository<MeasurementSubjectModel>>();
			builder.RegisterType<Repository<ProfileModel>>().As<IRepository<ProfileModel>>();


			builder.RegisterType<NavigationService> ();
			builder.RegisterType<DashboardPage> ();
			builder.RegisterType<StatsPage> ();
			builder.RegisterType<StatsViewModel> ();

			builder.RegisterType<UnitOfWork> ().As<IUnitOfWork> ();

			builder.RegisterType<PreferencesPage>();
			builder.RegisterType<Preference> ();

			builder.RegisterType<MeasurementPage> ();
			builder.RegisterType<MeasurementViewModel> ();

			builder.RegisterType<MeasurementListPage> ();
			builder.RegisterType<MeasurementListViewModel> ();

			builder.RegisterType<FilterPage> ();
			builder.RegisterType<FilterViewModel> ();

			builder.RegisterType<AvgMeasurement> ()
				.As < IPredefinedQuery<MeasurementInstanceModel>> ()
				.Keyed< IPredefinedQuery<MeasurementInstanceModel>> ("Average");

			builder.RegisterType<LastEntryMeasurement> ()
				.As < IPredefinedQuery<MeasurementInstanceModel>> ()
				.Keyed< IPredefinedQuery<MeasurementInstanceModel>> ("Last Entry");

			builder.RegisterType<MinMeasurement> ()
				.As < IPredefinedQuery<MeasurementInstanceModel>> ()
				.Keyed< IPredefinedQuery<MeasurementInstanceModel>> ("Minimum observed");

			builder.RegisterType<MaxMeasurement> ()
				.As < IPredefinedQuery<MeasurementInstanceModel>> ()
				.Keyed< IPredefinedQuery<MeasurementInstanceModel>> ("Maximum observed");

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




		public static Page GetMainPage ()
		{	

			Initialization.InitializeDatabase ();

			var mainNav = new MainMenu ();
			NavigationService.Navigation = mainNav.Detail.Navigation;

			////var mainNav = new NavigationPage (_container.Resolve<HomePage>("HomePage"));

			////NavigationService.Navigation = mainNav.Navigation;

			var tabPage = new TabbedPage ();
			tabPage.Children.Add (mainNav);

			var secondPage = new AllMeasurements ();
			tabPage.Children.Add (secondPage);


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

