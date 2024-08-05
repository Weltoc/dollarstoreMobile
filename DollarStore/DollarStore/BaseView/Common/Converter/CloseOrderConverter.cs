using System;
using System.Globalization;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class CloseOrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string activityStatus = (string)value;
            bool visibility = false;

            if(activityStatus == "Pending")
            {
                visibility = true;
            }

            return visibility;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
