using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class ImageNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imgValue = (string)value;
            string backValue = "";
            if (string.IsNullOrEmpty(imgValue))
            {
                backValue = "photo.png";
            }
            else
            {
                backValue = imgValue;
            }
            return backValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
