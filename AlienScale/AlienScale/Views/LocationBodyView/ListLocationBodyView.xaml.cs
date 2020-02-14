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
	public partial class ListLocationBodyView : StackLayout
	{
        LocationViewModel _viewModel;

        public ListLocationBodyView ()
		{
			InitializeComponent ();
		}

        public ListLocationBodyView(LocationViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            BindingContext = _viewModel;
        }
	}
}