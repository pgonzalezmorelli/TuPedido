using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace TuPedido.Extensions
{
    public class EmptyCollectionToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            var valueType = value.GetType();
            if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                throw new InvalidOperationException("The target must be an IEnumerable<>");
            
            return ((IEnumerable)value).GetEnumerator().MoveNext();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
