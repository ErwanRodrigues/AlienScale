using AlienScale.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlienScale.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapBodyView : StackLayout
	{
        MapViewModel _viewModel;

        public MapBodyView ()
		{
			InitializeComponent ();
		}
        public MapBodyView(HomeViewModel viewModel)
        {
            this._viewModel = new MapViewModel(viewModel);
            InitializeComponent();
            BindingContext = _viewModel;
        }
	}
}