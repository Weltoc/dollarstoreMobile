using System;
using System.Globalization;
using Xamarin.Forms;

namespace DollarStore.Common.Converter
{
    public class PurchaseTotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Text = "{Binding Source={x:Static local:OrderPurchaseList.Instance}, Path=Total}"
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
