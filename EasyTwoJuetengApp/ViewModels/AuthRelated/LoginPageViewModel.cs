using EasyTwoJuetengApp.Helpers;
using EasyTwoJuetengApp.Interfaces;
using EasyTwoJuetengApp.Models.AuthRelated;
using EasyTwoJuetengApp.Views;
using Plugin.Toast;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EasyTwoJuetengApp.ViewModels.AuthRelated
{
    public class LoginPageViewModel : ViewModelBase
    {
        private LoginCredential _loginCredentials = new LoginCredential();
        private readonly INavigationService _navigationService;
        private readonly IPostAsync<LoginCredential, LoginResult> _authService;

        public string Username 
        {
            get => _loginCredentials.Username ?? "";
            set
            {
                _loginCredentials.Username = value;
            }
        }
        public string Password 
        {
            get => _loginCredentials.Password ?? "";
            set
            {
                _loginCredentials.Password = value;
            }
        }
        private bool _isControlEnable;
        public bool IsControlEnable 
        {
            get => _isControlEnable;
            set
            {
                _isControlEnable = value;
                RaisePropertyChanged();
            }
        }
        public LoginPageViewModel(INavigationService navigationService,
            IPostAsync<LoginCredential,LoginResult> authService)
            :base(navigationService)
        {
            _navigationService = navigationService;
            _authService = authService;
            _isControlEnable = true;
        }

        [Obsolete]
        public DelegateCommand LoginCommand => new DelegateCommand(async () =>
        {
            _isControlEnable = false;
            await UIHelper.Load(LoginTask());
          
        });
        private async Task LoginTask()
        {
            var authLoginResult = await _authService.PostAsync(_loginCredentials);
            if (authLoginResult == null)
            {
                _isControlEnable = true;
                CrossToastPopUp.Current.ShowToastMessage("Username or Password Invalid");
            }
            else
            {
                await SetCurrentLogginedUserProperties(authLoginResult);
                App.Current.MainPage = new MainPage();
            }
        }
        private async Task SetCurrentLogginedUserProperties(LoginResult loginResult)
        {
            App.Current.Properties["Token"] = loginResult.Token;
            App.Current.Properties["Nickname"] = loginResult.Nickname;
            App.Current.Properties["FullName"] = loginResult.FullName;
            await App.Current.SavePropertiesAsync();
        }
        public DelegateCommand ResetCommand => new DelegateCommand(() =>
        {
            Username = "";
            Password = "";
            RaisePropertyChanged(nameof(Username));
            RaisePropertyChanged(nameof(Password));
        });
    }
}
