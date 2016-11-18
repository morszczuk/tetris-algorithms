using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Media;
using Tetris.Models;

namespace Tetris.Converters
{
    class WellStateToVisual : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as WellState;



            //var partWidth = (500) / item.Well.Width;
            //var partHeight = (500) / item.Fill.Count;

            //var parts = new List<System.Windows.Shapes.Rectangle>();
            var buttons = new List<Button>();

            for (var y = item.Fill.Count - 1; y >= 0; y--)
            {
                for (var x = 0; x < item.Well.Width; x++)
                {
                    if (item.Fill[y][x])
                    {

                        Button b = new Button()
                        {
                            Background = new SolidColorBrush(Colors.Black),
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                        };

                        Grid.SetRow(b,item.Fill.Count -1 -  y);
                        Grid.SetColumn(b, x);
                        buttons.Add(b);
                    }
                }
            }
            return buttons;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
