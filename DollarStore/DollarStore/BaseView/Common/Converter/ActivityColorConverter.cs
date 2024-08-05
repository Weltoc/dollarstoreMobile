using System;
using System.Globalization;
using Xamarin.Forms;

namespace DollarStore.Converter
{
    public class ActivityColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ActivityStatus = (string)value;
            string color;
            switch (ActivityStatus)
            {
                case "Pending":
                    color = "#a1a1a1";
                    break;
                case "New":
                    color = "#FFB90B";
                    break;
                case "Closed":
                    color = "#0FA103";
                    break;
                case "Shipped":
                    color = "#1C1C1C";
                    break;
                case "InShipping":
                    color = "#FFB90B";
                    break;
                case "Received":
                    color = "#FFB90B";
                    break; 
                case "In Pick Up":
                    color = "#0040FF";
                    break;
                case "In Delivery":
                    color = "#0040FF";
                    break;
                case "Pick Up":
                    color = "#2E64FE";
                    break;
                case "Walk In":
                    color = "#31B404";
                    break;
                case "Delivery":
                    color = "#FF8000";
                    break;
                case "Open":
                    color = "#075E54";
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
