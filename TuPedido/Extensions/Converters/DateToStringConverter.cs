using System;
using Xamarin.Forms;

namespace TuPedido.Extensions
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is DateTime))
                throw new InvalidOperationException("The target must be a datetime");

            return ((DateTime)value).ToString("dd/MM/yyyy HH:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
