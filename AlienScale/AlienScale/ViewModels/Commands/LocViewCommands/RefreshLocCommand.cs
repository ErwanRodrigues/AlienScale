using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands.LocViewCommands
{
    public class RefreshLocCommand : ICommand
    {
        LocationViewModel _viewModel;

        public RefreshLocCommand(LocationViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.HomeViewModel.UpdateLocs();
            _viewModel.LocIsRefreshing = false;
        }
    }
}
