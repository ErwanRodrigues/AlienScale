using AlienScale.Models;
using AlienScale.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlienScale.Views.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSessionPopup : PopupPage
    {
        SessionViewModel _viewModel;
        public AddSessionPopup(SessionViewModel viewModel)
        {
            InitializeComponent();
            this._viewModel = viewModel;
            _viewModel.CurrentSession = new Session();
            this.BindingContext = _viewModel;
        }
    }
}