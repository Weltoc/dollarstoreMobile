using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class ReceiviedPoStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? received = (bool?)value;

            string receivedStatus = "Not Received";
            if(received == true) {
                return "Received";
            }
            if(received == null) { return "Not Received"; }
            return receivedStatus;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
