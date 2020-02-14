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
    public partial class AddLocationPopup : PopupPage
    {
        LocationViewModel _viewModel;
        public AddLocationPopup(LocationViewModel viewModel)
        {
            InitializeComponent();
            this._viewModel = viewModel;
            _viewModel.CurrentLoc = new FishingLoc();
            this.BindingContext = _viewModel;
        }
    }
}