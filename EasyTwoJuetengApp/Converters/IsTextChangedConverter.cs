using ImTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace EasyTwoJuetengApp.Converters
{
    public class IsTextChangedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (string)value ?? "";
            var currentInput = ((parameter as Binding).Source as InputView).Text ?? string.Empty;
            return currentInput.ToLower() != input.ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value;
        }
    }
}