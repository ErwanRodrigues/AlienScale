using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands
{
    public class RegisterNavigationCommand : ICommand
    {
        private LoginViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public RegisterNavigationCommand(LoginViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        public void Execute(object parameter)
        {
            viewModel.Navigate();
        }
    }
}
