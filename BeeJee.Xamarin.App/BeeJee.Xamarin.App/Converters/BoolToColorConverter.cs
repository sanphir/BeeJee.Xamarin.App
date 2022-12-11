using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace BeeJee.Xamarin.App.Converters
{
    /// <summary>
    /// Конвертер для конвертации bool значения в цвет
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует значение от контекста к вьюхе
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Параметры для замены True/False указываются через разделитель | </param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] parameters = (parameter as string).Split(new char[] { '|' });

            if (parameters.Count() < 2) throw new ArgumentException("Недостаточно параметров", nameof(parameter));

            var converter = new ColorTypeConverter();

            var trueColorValue = parameters[0];
            Color trueColor = trueColorValue.StartsWith("#") ? Color.FromHex(trueColorValue) : (Color)converter.ConvertFromInvariantString(trueColorValue);

            var falseColorValue = parameters[1];
            Color falseColor = trueColorValue.StartsWith("#") ? Color.FromHex(falseColorValue) : (Color)converter.ConvertFromInvariantString(falseColorValue);

            return (bool)value ? trueColor : falseColor;
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
