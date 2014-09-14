using System;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using Xamarin.Forms;

namespace Meezure
{
	public class ToolbarViewModel : ViewModelBase
	{
		public ToolbarViewModel ()
		{
			this.GoToPreferences = new Command (() => App.NavigationService.NavigateTo("PreferencesPage"));
			this.HideButton = new Command (() => IsVisible = !IsVisible);
			IsVisible = true;
			IsAlwaysVisible = true;
		}

		private bool _isVisible;
		public bool IsVisible {
			get {
				return _isVisible;
			}
			set {
				Set (() => IsVisible, ref _isVisible, value);

			}
		}

		private bool _isAlwaysVisible;
		public bool IsAlwaysVisible {
			get {
				return _isAlwaysVisible;
			}
			set {
				Set (() => IsAlwaysVisible, ref _isAlwaysVisible, value);

			}
		}

		public ICommand GoToPreferences { internal set; get; }
		public ICommand HideButton { internal set; get; }

	}
}

