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
	public partial class StatLocationBodyView : StackLayout
	{
        LocationViewModel _viewModel;
        public StatLocationBodyView ()
		{
			InitializeComponent ();
		}

        public StatLocationBodyView(LocationViewModel viewModel)
        {
            this._viewModel = viewModel;
            InitializeComponent();
            BindingContext = _viewModel;
        }
	}
}