using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace MeasureONE
{
	public class MenuViewModel : ViewModelBase
	{
		public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
		public MenuViewModel ()
		{
			MenuItems = new ObservableCollection<MenuItemViewModel> ();

			MenuItems.Add (new MenuItemViewModel{Title="Test1", MenuType="Measurement" });
			MenuItems.Add (new MenuItemViewModel{Title="Test2", MenuType="Measurement" });
		}
	}

	public class MenuItemViewModel : ViewModelBase
	{
		public MenuItemViewModel ()
		{
		}

		private string _title;
		public string Title 
		{
			get{ return _title; }
			set{ Set (() => Title, ref _title, value);}
		}

		private string _details;
		public string Details
		{ 
			get{ return _details;}
			set{ Set (() => Details, ref _details, value);} 
		}

		private int _id;
		public int Id 
		{ 
			get{ return _id;}
			set{ Set (() => Id, ref _id, value);} 
		}

		private string _icon;
		public string Icon 
		{ 
			get{ return _icon;}
			set{ Set (() => Icon, ref _icon, value);} 
		}

		private string _menuType;
		public string MenuType
		{ 
			get{ return _menuType;}
			set{ Set (() => MenuType, ref _menuType, value);} 
		}
	}
}

