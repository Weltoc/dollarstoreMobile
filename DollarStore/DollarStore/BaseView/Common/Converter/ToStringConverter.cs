
using System;
using System.Globalization;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class ToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = "Total item :" + (string)value.ToString();

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
