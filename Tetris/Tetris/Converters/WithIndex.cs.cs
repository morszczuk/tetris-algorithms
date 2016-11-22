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
    public class WithIndex : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var states = value as IEnumerable<object>;
            var result = new List<Tuple<object, int>>();
            if (states == null) return result;
            var i = 0;
            foreach (var state in states)
            {
                result.Add(new Tuple<object, int>(state, i++));
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
