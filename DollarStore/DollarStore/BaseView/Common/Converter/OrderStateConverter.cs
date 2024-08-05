using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.BaseView.Common.Converter
{
    public class OrderStateColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string OrderState = (string)value;
            string color;
            switch (OrderState)
            {
                case "pending":
                    color = "#a1a1a1";
                    break;
                case "closed":
                    color = "#FFB90B";
                    break;
                default:
                    color = "#FFF";
                    break;

            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
