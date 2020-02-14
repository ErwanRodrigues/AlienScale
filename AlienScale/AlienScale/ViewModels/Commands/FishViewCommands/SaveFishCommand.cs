using AlienScale.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands.FishViewCommands
{
    public class SaveFishCommand : ICommand
    {
        FishViewModel _viewModel;

        public event EventHandler CanExecuteChanged;

        public SaveFishCommand(FishViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            Fish fish = (Fish)parameter;
            if (fish == null)
                return false;

            if (fish.Weight > 0 && fish.FishType != null)
                return true;
            
            return false;
        }

        public async void Execute(object parameter)
        {
            if(_viewModel.HomeViewModel.RunningSession.IsRunning == true)
            {
                //fish should be added link with a session
                await _viewModel.InsertFishInSession((Fish)parameter);
            }
            else
            {
                //fish should be added out of any session
                await _viewModel.InsertFish((Fish)parameter);
            }
            await PopupNavigation.Instance.PopAsync(true);
            //_viewModel.SelectedFishTab = 0;
            //_viewModel.OptionSelectionChangedCommand.Execute(_viewModel.SelectedFishTab);
        }
    }
}
