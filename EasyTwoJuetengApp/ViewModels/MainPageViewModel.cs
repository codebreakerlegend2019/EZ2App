using EasyTwoJuetengApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EasyTwoJuetengApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _nickName;
        private readonly INavigationService _navigationService;

        public string Nickname
        {
            get => _nickName ?? "";
            set
            {
                _nickName = value;
                RaisePropertyChanged();
            }
        }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _nickName = App.CurrentlyLoginnedUser().Nickname;
            _navigationService = navigationService;
        }

        public DelegateCommand LogoutCommand => new DelegateCommand(async () =>
        {
            Application.Current.Properties["Token"] = string.Empty;
            Application.Current.Properties["Nickname"] = string.Empty;
            Application.Current.Properties["FullName"] = string.Empty;
            await Application.Current.SavePropertiesAsync();
            App.Current.MainPage = new LoginPage();
        });
    }
}
