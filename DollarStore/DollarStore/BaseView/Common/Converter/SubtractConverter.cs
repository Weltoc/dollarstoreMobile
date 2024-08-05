using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.BaseView.Common.Converter
{
    public class SubtractConverter : IValueConverter
    {
        //public string Value { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = System.Convert.ToInt32(value);
            // Change here if you want other operations
            return val - System.Convert.ToInt32(parameter);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
