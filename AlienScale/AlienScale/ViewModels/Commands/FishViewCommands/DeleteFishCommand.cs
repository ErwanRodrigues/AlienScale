using AlienScale.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands.FishViewCommands
{
    public class DeleteFishCommand : ICommand
    {
        FishViewModel _viewModel;

        public DeleteFishCommand(FishViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_viewModel.CurrentFish != null)
                return true;

            return false;
        }

        public void Execute(object parameter)
        {
            if(parameter != null)
            {
                Fish fh = (Fish)parameter;
                Fish.Delete(fh);
                _viewModel.HomeViewModel.UpdateFishes();
                _viewModel.HomeViewModel.UpdateSessions();
            }
        }
    }
}
