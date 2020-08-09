using EasyTwoJuetengApp.Views.CustomerRelatedPages;
using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace EasyTwoJuetengApp.Views
{
    public partial class MainPage : SharedTransitionShell
    {
        public MainPage()
        {
            InitializeComponent();
            Routing.RegisterRoute("CustomerPage", typeof(CustomerPage));
            Routing.RegisterRoute("AddOrEditCustomerPage", typeof(AddOrEditCustomerPage));
        }
    }
}