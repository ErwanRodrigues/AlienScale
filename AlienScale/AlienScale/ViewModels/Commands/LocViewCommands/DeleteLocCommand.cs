using AlienScale.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands.LocViewCommands
{
    public class DeleteLocCommand : ICommand
    {
        LocationViewModel _viewModel;

        public DeleteLocCommand(LocationViewModel viewModel)
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
            FishingLoc loc = (FishingLoc)parameter;
            if(loc != null)
            {
                _viewModel.DeleteLoc(loc);
            }
        }
    }
}
