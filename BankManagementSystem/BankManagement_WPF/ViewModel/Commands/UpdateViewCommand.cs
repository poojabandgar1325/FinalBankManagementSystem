using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankManagement_WPF.ViewModel.Commands
{
    class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel; 
        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.ToString() == "Apply")
            {
                viewModel.SelectedViewModel = new ApplyViewModel();
            }
            else if (parameter.ToString() == "Previous")
            {
                viewModel.SelectedViewModel = new PreviousViewModel();
            }
        }
    }
}
