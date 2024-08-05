using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class ReceivedColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? status = (bool?)value;

            string color = "#FF0040";

            switch (status)
            {
                case false:
                    color = "#FF0040";
                    break;
                case true:
                    color = "#04B404";
                    break;
                default:
                    break;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
