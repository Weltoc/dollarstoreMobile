using System;
using System.Globalization;
using Xamarin.Forms;


namespace DollarStore.BaseView.Common.Converter
{
    public class DateShort : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Outdate = "";
            if (value != null)
            {
                DateTime Entrydate = (DateTime)value;
                Outdate = Entrydate.ToShortDateString();
            }

            return Outdate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

