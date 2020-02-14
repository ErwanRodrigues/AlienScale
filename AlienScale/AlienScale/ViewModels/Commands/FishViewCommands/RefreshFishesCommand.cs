using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands.FishViewCommands
{
    public class RefreshFishesCommand : ICommand
    {
        FishViewModel ViewModel { get; set; }

        public RefreshFishesCommand(FishViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.HomeViewModel.UpdateFishes();
            ViewModel.FishIsRefreshing = false;
        }
    }
}
