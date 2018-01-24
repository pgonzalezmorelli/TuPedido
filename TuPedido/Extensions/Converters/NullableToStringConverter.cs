using System;
using Xamarin.Forms;

namespace TuPedido.Extensions
{
    public class NullableToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var unavailable = "No disponible";

            if (value == null)
                return "No disponible";

            if (value is DateTime?)
                return ((DateTime?)value).HasValue ? new DateToStringConverter().Convert(value, targetType, parameter, culture) : unavailable;

            if (value is int?)
                return ((int?)value).HasValue ? $"{value.ToString()} minutos" : unavailable;

            if (value is string)
                return value;

            throw new InvalidOperationException("The target must be a datetime? or int?");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
