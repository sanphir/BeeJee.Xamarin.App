using BeeJee.Xamarin.App.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.Converters
{
    public class SortDirectionToTextConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует значение от контекста к вьюхе
        /// </summary>
        /// <param name="value">Направление сортировки <see cref="SortDirection"/></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SortDirection sortDirection = (SortDirection)value;
            return sortDirection == SortDirection.ASC ? ">" : "<";
        }

        /// <summary>
        /// метод обратного преобразования значения от вьюхи к контексту
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
