using AlienScale.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlienScale.Views.FishBodyView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FishBodyView : StackLayout
	{
        FishViewModel _viewModel;

        public FishBodyView ()
		{
            InitializeComponent();
        }

        public FishBodyView(HomeViewModel viewModel)
        {
            InitializeComponent();
            this._viewModel = new FishViewModel(viewModel);
            BindingContext = _viewModel;
        }
	}
}