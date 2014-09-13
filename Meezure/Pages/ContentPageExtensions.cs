using System;
using Xamarin.Forms;

namespace MeasureONE
{
	public static class ContentPageExtensions
	{
		public static void SetToolbarBindingContext(this ContentPage page){
			var vm = new ToolbarViewModel ();
			foreach (var item in page.ToolbarItems) {
				item.BindingContext = vm;
			}
		}
	}

}

