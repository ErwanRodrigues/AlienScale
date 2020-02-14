using AlienScale.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands
{
    public class RegisterCommand : ICommand
    {
        public RegisterViewModel viewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public RegisterCommand(RegisterViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            User user = (User)parameter;
            if (user == null)
                return false;

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }
            else
            {
                if (user.Password == user.ConfirmPassword)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async void Execute(object parameter)
        {
            User user = (User)parameter;
            await viewModel.Register();
        }
    }
}
