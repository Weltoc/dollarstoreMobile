using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Outval = "";
            if (value != null)
            {
                string Entryval = (string)value;
                if (Entryval == "FCFA")
                {
                    Outval = "FCFA";
                }
                else if (Entryval == "DOLLAR")
                {
                    Outval = "$";
                }
                
                else if (Entryval == "EURO")
                {
                    Outval = "€";
                }

            }
            if (value == null)
            {
                Outval = "$";
            }
            return Outval;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
