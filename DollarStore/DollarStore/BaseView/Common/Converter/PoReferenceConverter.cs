using System;
using System.Globalization;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    class PoReferenceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Outval = "";
            if (value != null)
            {
                int Entryval = (int)value;
                if(Entryval < 10)
                {
                    Outval = "0000000" + Entryval.ToString();
                }
                else if(Entryval >= 10 && Entryval < 100)
                {
                    Outval = "000000" + Entryval.ToString();
                }
                else if (Entryval >= 100 && Entryval < 1000)
                {
                    Outval = "00000" + Entryval.ToString();
                }
                else if (Entryval >= 1000 && Entryval < 10000)
                {
                    Outval = "0000" + Entryval.ToString();
                }
                else if (Entryval >= 10000 && Entryval < 100000)
                {
                    Outval = "000" + Entryval.ToString();
                }
                else if (Entryval >= 100000 && Entryval < 1000000)
                {
                    Outval = "00" + Entryval.ToString();
                }
                else if (Entryval >= 1000000 && Entryval < 10000000)
                {
                    Outval = "0" + Entryval.ToString();
                }
                else
                {
                    Outval = Entryval.ToString();
                }
            }

            return Outval;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
