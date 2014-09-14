using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Autofac;

namespace Meezure
{	
	public partial class FilterPage : ContentPage
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

		public FilterPage ()
		{
			this.Icon = "slideout.png";
			this.Title = "Filter";

			_scope = App.AutoFacContainer.BeginLifetimeScope();

			var vm = _scope.Resolve<FilterViewModel> ();

			BindingContext = vm;

			var layout = new StackLayout ();

			layout.Children.Add (new Label() {Text = "Enter a filter"});
			layout.Children.Add (new Label() {Text = "Subject"});
			var subjectEntry = new Entry();
			subjectEntry.SetBinding (Entry.TextProperty, "Subject");
			layout.Children.Add (subjectEntry);
			var button = new Button () { Text = "Apply Filter" };
			button.SetBinding (Button.CommandProperty, "FilterMeasures");
			layout.Children.Add (button);

			Content = layout;


		}

	}
}

