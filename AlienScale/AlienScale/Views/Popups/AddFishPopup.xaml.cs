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
	public partial class AddFishPopup : PopupPage
    {
        FishViewModel _viewModel;

        public AddFishPopup()
        {

        }

        public AddFishPopup (FishViewModel viewModel)
		{
			InitializeComponent ();
            this._viewModel = viewModel;
            _viewModel.CurrentFish = new Fish();
            this.BindingContext = _viewModel;

           
		}
	}
}