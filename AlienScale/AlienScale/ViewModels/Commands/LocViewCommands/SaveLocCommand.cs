using AlienScale.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands.LocViewCommands
{
    public class SaveLocCommand : ICommand
    {
        LocationViewModel _viewModel;

        public SaveLocCommand(LocationViewModel viewModel)
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
            FishingLoc loc = (FishingLoc)parameter;
            if(loc != null)
            {
                await _viewModel.InsertLoc(loc);
                await PopupNavigation.Instance.PopAsync(true);
                //_viewModel.SelectedLocTab = 0;
                //_viewModel.OptionSelectionChangedCommand.Execute(_viewModel.SelectedLocTab);
            }
        }
    }
}
