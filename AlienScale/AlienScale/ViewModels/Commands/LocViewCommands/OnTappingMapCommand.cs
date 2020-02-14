using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlienScale.ViewModels.Commands.LocViewCommands
{
    public class OnTappingMapCommand : ICommand
    {
        public LocationViewModel _viewModel { get; set; }

        public OnTappingMapCommand(LocationViewModel viewModel)
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
            Task.Delay(200);
        }
    }
}
