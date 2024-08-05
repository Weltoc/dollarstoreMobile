using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.BaseView.Common.Converter
{
    class TimestampToDateTimeToStringConverter : IValueConverter
    {      
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Outdate;
            if (value != null)
            { 
                Timestamp Entrydate = (Timestamp)value; 

                var x = Entrydate.ToDateTime().Date.Year.ToString(); 
                Outdate = Entrydate.ToDateTime().ToString("MM/dd/yyyy HH:mm:ss zzz");

                if (x == "1")
                {
                    Outdate = "--/--/--";
                }
                else if (x == "1970")
                {
                    Outdate = "Now";
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
