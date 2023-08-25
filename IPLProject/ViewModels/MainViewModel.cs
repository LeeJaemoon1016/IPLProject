using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using IPLProject.Models;
using IPLProject.Repositories;

namespace IPLProject.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository userRepository;

        //Properties
        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }


        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        // --> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowTestViewCommand { get; }
        public ICommand ShowSettingViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowSettingViewCommand = new ViewModelCommand(ExecuteShowSettingViewCommand);


            //Default view
            ExecuteShowHomeViewCommand(null);
            

            LoadCurrentUserData();
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void ExecuteShowSettingViewCommand(object obj)
        {
            //Admin 계정으로 접속시에만 Setting 창 보이고, 이외엔 Setting창 안보이게.
            if (Thread.CurrentPrincipal.Identity.Name == "admin")
            {
                CurrentChildView = new SettingViewModel();
                Caption = "Setting";
                Icon = IconChar.Tools;
            }
            else
            {
                CurrentChildView = new NullSettingViewModel();
                Caption = "Setting";
                Icon = IconChar.Tools;
            }
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            // 데이터 베이스 사용하지 않으므로 주석 처리
            //if (user != null)
            //{
            //    CurrentUserAccount.Username = user.Username;
            //    CurrentUserAccount.DisplayName = $"{user.Name} {user.LastName} ;)";
            //    CurrentUserAccount.ProfilePicture = null;
            //}
            //else
            //{
            //    CurrentUserAccount.DisplayName = "User not logged in";
            //    //Hide child views.
            //}

            //대신 로그인 한 ID에 따라 사용자 정보 메인인터페이스에 띄우도록
            if (Thread.CurrentPrincipal.Identity.Name == "admin")
            {
                CurrentUserAccount.DisplayName = "Log in as an Admin Account.";
            }
            else if (Thread.CurrentPrincipal.Identity.Name == "userA")
            {
                CurrentUserAccount.DisplayName = "Log in as an User A Account.";
            }
            else if (Thread.CurrentPrincipal.Identity.Name == "userB")
            {
                CurrentUserAccount.DisplayName = "Log in as an User B Account.";
            }
            else
            {
                CurrentUserAccount.DisplayName = "User not logged in";
                //Hide child views.
            }
        }
    }
}
