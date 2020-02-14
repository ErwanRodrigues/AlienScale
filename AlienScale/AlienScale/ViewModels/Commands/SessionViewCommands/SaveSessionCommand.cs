using AlienScale.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands.SessionViewCommands
{
    public class SaveSessionCommand : ICommand
    {
        SessionViewModel _viewModel;
        public SaveSessionCommand(SessionViewModel viewModel)
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
            if(parameter != null)
            {
                Session sess = (Session)parameter;
                await _viewModel.InsertSession(sess);
                await PopupNavigation.Instance.PopAsync(true);
                //_viewModel.SelectedSessionTab = 0;
                //_viewModel.OptionSelectionChangedCommand.Execute(_viewModel.SelectedSessionTab);
            }
        }
    }
}
