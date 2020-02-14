using AlienScale.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlienScale.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        RegisterViewModel _viewModel;

        public RegisterPage ()
		{
            InitializeComponent();
            _viewModel = new RegisterViewModel();

            BindingContext = _viewModel;
        }
	}
}