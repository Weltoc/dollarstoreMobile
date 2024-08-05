using System;
using System.Globalization;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class DateTimeConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Outdate; 
            if (value != null)
            {
                DateTime Entrydate = (DateTime)value;
                //var a = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now)

                //FieldValue Entrydate = (FieldValue)value;

                var x = Entrydate.Date.Year.ToString();

                //.ToLocalTime()
                Outdate = Entrydate.ToString("MM/dd/yyyy HH:mm:ss zzz");

                if(x == "1")
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
