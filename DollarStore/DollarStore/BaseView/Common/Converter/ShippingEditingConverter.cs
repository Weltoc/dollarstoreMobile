using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class ShippingEditingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Activitystatus = (string)value;
            bool editable;
            switch (Activitystatus)
            {
                case "InShipping":
                    editable = true;
                    break;
                case "Shipped":
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
