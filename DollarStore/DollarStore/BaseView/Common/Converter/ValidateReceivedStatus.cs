using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class ValidateReceivedStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? received = (bool?)value;

            string receivedStatus = "Not Validated";
            if (received == true)
            {
                return "Validated";
            }
            if(received == null)
            {
                return "Not Validated";
            }
            return receivedStatus;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
