using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tetris.Models;

namespace Tetris.Converters
{
    /// <summary>
    /// Converter for generating information about well such as space covered height and width
    /// </summary>
    public class WellStateToStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as WellState;
            if (item == null) return "";
            var text = $"Szerokość: {item.Well.Width}, wysokość: {item.Fill.Count} pokrycie: {item.PercentageFilled}%";
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
