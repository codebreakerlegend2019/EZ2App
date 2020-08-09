using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTwoJuetengApp.ViewModels
{
    public class CustomBaseViewModel : BindableBase
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, nameof(Title)); }
        }
    }
}