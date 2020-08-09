using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyTwoJuetengApp.ViewModels.EntriesRelated
{
    public class EntriesPageViewModel : ViewModelBase
    {
        public EntriesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Entries";
        }
    }
}
