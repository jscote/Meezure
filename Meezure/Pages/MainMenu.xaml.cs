using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using Autofac;
using GalaSoft.MvvmLight.Messaging;

namespace MeasureONE
{	
	public partial class MainMenu : MasterDetailPage
	{	
		private ILifetimeScope _scope;

		private string _loadingMsgId = string.Format ("Loading:{0}", MeasurementPage.PageName.Remove (MeasurementPage.PageName.IndexOf ("Page")));

		private MainMenuView _master;
		public MainMenu ()
		{
			InitializeComponent ();

			Master = _master = new MainMenuView ();

			_scope = App.AutoFacContainer.BeginLifetimeScope ();

			var mainNav = new NavigationPage (_scope.Resolve<MeasurementPage>());

			var msg = new NotificationMessage<IDictionary<string, int>>(new Dictionary<string, int>(), _loadingMsgId);
			msg.Content.Add("Mode", 0);

			Messenger.Default.Send<NotificationMessage<IDictionary<string, int>>> (msg, _loadingMsgId);

			Detail = mainNav;

			this.Icon = "slideout.png";

			_master.PageSelectionChanged = (measurementTypeDefId) => {

				IDictionary<string, int> msgSelectionChanged = new Dictionary<string, int>();
				msgSelectionChanged.Add("DefinitionId", measurementTypeDefId);
				msgSelectionChanged.Add("Mode", 0);
				var page = App.NavigationService.MasterNavigateTo (MeasurementPage.PageName, msgSelectionChanged);
				Detail = new NavigationPage(page);
				Detail.Title = page.Title;
				IsPresented = false;
			};
		}

		protected override void OnAppearing ()
		{
			Messenger.Default.Send<NotificationMessage> (new NotificationMessage("Loading:Dashboard"), "Loading:Dashboard");
		}
	}

	public class MainMenuView: ContentPage
	{
		private ILifetimeScope _scope;

		public Action<int> PageSelectionChanged;
		private int selectedMeasurementTypeDefId;
		public int PageSelection {
			get{ return selectedMeasurementTypeDefId; }
			set { selectedMeasurementTypeDefId = value; 
				if (PageSelectionChanged != null)
					PageSelectionChanged (selectedMeasurementTypeDefId);
			}
		}

		public MainMenuView ()
		{
			this.Icon = "slideout.png";
			this.Title = "Menu";

			var layout = new StackLayout { Spacing = 0 };

			var label = new ContentView {
				Padding = new Thickness(10, 36, 0, 5),
				BackgroundColor = Color.Transparent,
				Content = new Label {
					Text = "Popular",
					Font = Font.SystemFontOfSize (NamedSize.Medium)
				}
			};

			layout.Children.Add (label);

			_scope = App.AutoFacContainer.BeginLifetimeScope();

			var vm = _scope.Resolve<MeasurementDashboardViewModel> ();

   			BindingContext = vm;

			var listView = new ListView ();

			var cell = new DataTemplate(typeof(MainMenuViewCell));

			listView.ItemTemplate = cell;

			listView.ItemsSource = vm.Items;

			listView.HasUnevenRows = true;

			layout.Children.Add (listView);

			listView.ItemTapped += (sender, args) =>
			{
				selectedMeasurementTypeDefId  = (args.Item as MeasurementDashboardItemViewModel).MeasurementDefinitionId;
				PageSelection = selectedMeasurementTypeDefId;

				//var form = ServiceLocator.Current.Resolve<>("");

				/*var menuItem = listView.SelectedItem as HomeMenuItem;
				menuType = menuItem.MenuType;
				switch(menuItem.MenuType){
				case MenuType.About:
					if(about == null)
						about = new AboutView();

					PageSelection = about;
					break;
				case MenuType.Blog:
					if(blog == null)
						blog = new BlogView();

					PageSelection = blog;
					break;
				case MenuType.Twitter:
					if(twitter == null)
						twitter = new TwitterView();

					PageSelection = twitter;
					break;
				}
*/
			};

			Content = layout;
		}

/*		protected override void OnAppearing ()
		{
			Messenger.Default.Send<NotificationMessage> (new NotificationMessage("Loading:Dashboard"), "Loading:Dashboard");

		}
*/

	}


}

