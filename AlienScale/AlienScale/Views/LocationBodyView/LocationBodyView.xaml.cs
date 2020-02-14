using AlienScale.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlienScale.Views.LocationBodyView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationBodyView : StackLayout
	{
        LocationViewModel _viewModel;

        public LocationBodyView ()
		{
			InitializeComponent ();
		}

        public LocationBodyView(HomeViewModel viewModel)
        {
            InitializeComponent();
            this._viewModel = new LocationViewModel(viewModel);
            BindingContext = _viewModel;
        }
	}
}