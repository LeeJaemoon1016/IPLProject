using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IPLProject.ViewModels
{
    public class NullSettingViewModel : ViewModelBase
    {
        private string _Address;

        public string Address
        {
            get
            {
                return _Address;
            }

            set
            {
                _Address = value;
                //OnPropertyChanged(nameof(Address));
            }
        }

        public ICommand ChangeCommand { get; }

        public NullSettingViewModel()
        {
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
        }

        private void ExecuteChangeCommand(object obj)
        {
            Address = "www.naver.com";
        }

        private bool CanExecuteChangeCommand(object obj)
        {
            return true;
        }
    }
}
