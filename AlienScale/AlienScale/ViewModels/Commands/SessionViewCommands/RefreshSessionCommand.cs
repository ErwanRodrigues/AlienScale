using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands.SessionViewCommands
{
    public class RefreshSessionCommand : ICommand
    {
        SessionViewModel _viewModel;

        public event EventHandler CanExecuteChanged;

        public RefreshSessionCommand(SessionViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.HomeViewModel.UpdateSessions();
            _viewModel.SessionIsRefreshing = false;
        }
    }
}
