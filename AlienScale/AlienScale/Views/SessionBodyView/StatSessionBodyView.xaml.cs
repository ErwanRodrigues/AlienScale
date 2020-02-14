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
	public partial class StatSessionBodyView : StackLayout
	{
        SessionViewModel _viewModel;

        public StatSessionBodyView ()
		{
			InitializeComponent ();
		}

        public StatSessionBodyView(SessionViewModel viewModel)
        {
            this._viewModel = viewModel;
            InitializeComponent();
            BindingContext = _viewModel;
        }
	}
}