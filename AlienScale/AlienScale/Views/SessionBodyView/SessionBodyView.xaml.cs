using AlienScale.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlienScale.Views.SessionBodyView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SessionBodyView : StackLayout
	{
        SessionViewModel _viewModel;

        public SessionBodyView ()
		{
			InitializeComponent ();
		}

        public SessionBodyView(HomeViewModel viewModel)
        {
            InitializeComponent();
            this._viewModel = new SessionViewModel(viewModel);
            BindingContext = _viewModel;
        }
	}
}