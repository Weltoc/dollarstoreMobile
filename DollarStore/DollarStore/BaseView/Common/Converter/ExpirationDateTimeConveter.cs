using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class ExpirationDateTimeConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Outdate;
            if (value != null)
            {
                DateTime Entrydate = (DateTime)value;
                var x = Entrydate.Date.Year.ToString();

                Outdate = Entrydate.ToString("MM/dd/yyyy zzz");

                if (x == "1")
                {
                    Outdate = "--/--/--";
                }

            }
            else
            {
                Outdate = "--/--/--";
            }

            return Outdate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
