using Prism;
using Prism.Ioc;
using EasyTwoJuetengApp.ViewModels;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EasyTwoJuetengApp.Helpers;
using System.Threading.Tasks;
using EasyTwoJuetengApp.Models.AuthRelated;
using ImTools;
using System;
using EasyTwoJuetengApp.Views.EntriesRelatedPages;
using EasyTwoJuetengApp.Views.CustomerRelatedPages;
using EasyTwoJuetengApp.Views;
using EasyTwoJuetengApp.Views.CustomPages;
using EasyTwoJuetengApp.ViewModels.AuthRelated;
using EasyTwoJuetengApp.ViewModels.EntriesRelated;
using EasyTwoJuetengApp.ViewModels.CustomerRelated;
using EasyTwoJuetengApp.ViewModels.CustomRelated;
using EasyTwoJuetengApp.Helpers.JdsHelpers;
using EasyTwoJuetengApp.Models.CustomerRelated;
using System.Collections.Generic;

namespace EasyTwoJuetengApp
{
    public partial class App
    {
#if (!DEBUG)
        private string _apiLink = "http://192.168.254.108:5449/";
#else
        private string _apiLink = "http://192.168.254.108:5449/";
#endif

        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        public static List<CustomerReadDto> DummyCustomers { get; set; }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            JdsClient.BaseAddress = _apiLink;
            Device.SetFlags(new[]
 {
                "SwipeView_Experimental",
                "RadioButton_Experimental",
                "IndicatorView_Experimental",
                "Expander_Experimental",
                "UseLegacyRenderers"
            });

            SetCacheDummyData();
            await RegisterLocalStorageProperties();
            await Route();
        }

        private async Task Route()
        {
            try
            {
                if (CurrentlyLoginnedUser().Token != string.Empty)
                {
                    var result = await NavigationService.NavigateAsync("MainPage");
                    if (!result.Success)
                    {
                        throw new Exception(result.Exception.Message);
                    }
                }
                else
                {
                    var result = await NavigationService.NavigateAsync("LoginPage");
                    if (!result.Success)
                    {
                        throw new Exception(result.Exception.Message);
                    }
                }
                SetPreviousPageReloadableToTrue();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool IsPageReloadable => Convert.ToBoolean(App.Current.Properties["IsPageReloadable"]);

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.AutoRegisterByInterFaceName("IPostAsync");
            containerRegistry.AutoRegisterByInterFaceName("IGetAsync");
            containerRegistry.AutoRegisterByInterFaceName("IPutAsync");
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<EntriesPage, EntriesPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomerPage, CustomerPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomTitleViewPage>();
            containerRegistry.RegisterForNavigation<AddOrEditCustomerPage, AddOrEditCustomerPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }

        private void SetCacheDummyData()
        {
            DummyCustomers = UIHelper.GenerateDummyDataFor1DModel<CustomerReadDto>(5);
        }

        private async Task RegisterLocalStorageProperties()
        {
            if (!Application.Current.Properties.ContainsKey("Token"))
                Application.Current.Properties.Add("Token", string.Empty);
            if (!Application.Current.Properties.ContainsKey("Nickname"))
                Application.Current.Properties.Add("Nickname", string.Empty);
            if (!Application.Current.Properties.ContainsKey("FullName"))
                Application.Current.Properties.Add("FullName", string.Empty);
            if (!Application.Current.Properties.ContainsKey("IsPageReloadable"))
                Application.Current.Properties.Add("IsPageReloadable", true);
            await Application.Current.SavePropertiesAsync();
        }

        public static void SetPreviousPageReloadableToTrue()
        {
            if (!Application.Current.Properties.ContainsKey("IsPageReloadable"))
                Application.Current.Properties.Add("IsPageReloadable", true);
            else
                Application.Current.Properties["IsPageReloadable"] = true;
        }

        public static void SetPreviousPageReloadableToFalse()
        {
            if (!Application.Current.Properties.ContainsKey("IsPageReloadable"))
                Application.Current.Properties.Add("IsPageReloadable", false);
            else
                Application.Current.Properties["IsPageReloadable"] = false;
        }

        public static CurrentLoginnedUser CurrentlyLoginnedUser()
        {
            return new CurrentLoginnedUser()
            {
                Nickname = Application.Current.Properties["Nickname"].ToString(),
                Token = Application.Current.Properties["Token"].ToString(),
                FullName = Application.Current.Properties["FullName"].ToString(),
            };
        }
    }
}