using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class EditinActionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Activitystatus = (string)value;
            bool editable;
            switch (Activitystatus)
            {
                case "New":
                    editable = true;
                    break;
                case "Pending":
                    editable = true;
                    break;
                case "Closed":
                    editable = false;
                    break;
                default:
                    editable = false;
                    break;
            }

            return editable;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
