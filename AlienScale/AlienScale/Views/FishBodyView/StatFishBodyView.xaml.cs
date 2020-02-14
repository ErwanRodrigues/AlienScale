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
	public partial class StatFishBodyView : StackLayout
	{
        FishViewModel _viewModel;

        public StatFishBodyView()
        {
            InitializeComponent();
        }

        public StatFishBodyView(FishViewModel viewModel)
        {
            this._viewModel = viewModel;
            InitializeComponent();
            BindingContext = _viewModel;
        }
    }
}