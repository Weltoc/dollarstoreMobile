using System;
using System.Globalization;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    public class NoteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Note = (string)value;
            bool hasnote;
            if (string.IsNullOrEmpty(Note))
            {
                hasnote = false;                
            }
            else
            {
                hasnote = true; 
            }

            return hasnote;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
