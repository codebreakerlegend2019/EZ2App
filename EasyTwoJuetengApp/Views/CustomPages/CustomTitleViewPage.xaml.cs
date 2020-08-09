using EasyTwoJuetengApp.Helpers;
using Prism.Commands;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTwoJuetengApp.Views.CustomPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTitleViewPage : ContentPage
    {
        public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomTitleViewPage), null);

        public static readonly BindableProperty IsToolBarButtonVisibleProperty =
           BindableProperty.Create(nameof(IsToolBarButtonVisible), typeof(bool), typeof(CustomTitleViewPage), null);

        public bool IsToolBarButtonVisible
        {
            get { return (bool)GetValue(IsToolBarButtonVisibleProperty); }
            set { SetValue(IsToolBarButtonVisibleProperty, value); }
        }

        public static readonly BindableProperty ToolBarIconProperty =
        BindableProperty.Create(nameof(ToolBarIcon), typeof(string), typeof(CustomTitleViewPage), null);

        public string ToolBarIcon
        {
            get { return (string)GetValue(ToolBarIconProperty) ?? MaterialFonts.Plus; }
            set { SetValue(ToolBarIconProperty, value); }
        }

        public static readonly BindableProperty ToolBarButtonCommandProperty =
      BindableProperty.Create(nameof(ToolBarButtonCommand), typeof(DelegateCommand), typeof(CustomTitleViewPage), null);

        public DelegateCommand ToolBarButtonCommand
        {
            get { return (DelegateCommand)GetValue(ToolBarButtonCommandProperty); }
            set { SetValue(ToolBarButtonCommandProperty, value); }
        }

        public CustomTitleViewPage()
        {
            InitializeComponent();
            TitleLabel.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
            ToolBarButton.SetBinding(Button.IsVisibleProperty, new Binding(nameof(IsToolBarButtonVisible), source: this));
            ToolBarButton.SetBinding(Button.TextProperty, new Binding(nameof(ToolBarIcon), source: this));
            ToolBarButton.SetBinding(Button.CommandProperty, new Binding(nameof(ToolBarButtonCommand), source: this));
        }
    }
}