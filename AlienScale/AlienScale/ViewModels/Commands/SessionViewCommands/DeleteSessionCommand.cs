using AlienScale.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands.SessionViewCommands
{
    public class DeleteSessionCommand : ICommand
    {
        SessionViewModel _viewModel;

        public DeleteSessionCommand(SessionViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            Session session = (Session)parameter;
            await _viewModel.DeleteSession(session);
            _viewModel.HomeViewModel.UpdateFishes();
            _viewModel.HomeViewModel.UpdateSessions();
        }
    }
}
