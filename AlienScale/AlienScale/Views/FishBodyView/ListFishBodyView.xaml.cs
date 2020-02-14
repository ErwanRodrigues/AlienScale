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
	public partial class ListFishBodyView : StackLayout
	{
        FishViewModel _viewModel;

        public ListFishBodyView ()
		{
			InitializeComponent ();
		}

        public ListFishBodyView(FishViewModel viewModel)
        {
            this._viewModel = viewModel;
            InitializeComponent();
            BindingContext = _viewModel;
        }
	}
}