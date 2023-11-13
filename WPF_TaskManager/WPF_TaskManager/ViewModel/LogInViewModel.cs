using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_TaskManager.Model;
using WPF_TaskManager.Repositories;

namespace WPF_TaskManager.ViewModel
{
    public class LogInViewModel:ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        // Properties
        public string Username 
        { 
            get => _username;
            set 
            {
                _username = value;
                OnPropertyChenged(nameof(Username));
            } 
        }
        public SecureString Password 
        { 
            get => _password;
            set
            {
                _password = value;
                OnPropertyChenged(nameof(Password));
            }
        }
        public string ErrorMessage 
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChenged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible 
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChenged(nameof(IsViewVisible));
            }
        }


        // Commands
        public ICommand LogInCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }


        // Constructor
        public LogInViewModel()
        {
            userRepository = new UserRepository();
            LogInCommand = new ViewModelCommand(ExecuteLogInCommand, CanExecuteLogInCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("",""));
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLogInCommand(object obj)
        {
            bool validDate;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validDate = false;
            else
                validDate = true;
            return validDate;
        }

        private void ExecuteLogInCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username,Password));
            if (isValidUser) 
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "** Invalid username or password";
            }
        }
    }
}
