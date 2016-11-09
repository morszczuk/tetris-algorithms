using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Tetris.Converters
{
    class BlockToVisual : IValueConverter
    {
        private static int _blocksMaxHeight = 8;
        private static int _blocksMaxWidth = 8;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as bool[,];

            List<System.Windows.Shapes.Rectangle> parts=new List<System.Windows.Shapes.Rectangle>();

            int partWidth = (200) / _blocksMaxWidth;
            int partHeight = (200) / _blocksMaxHeight;

            int startWidth = (_blocksMaxWidth - item.GetLength(1)) / 2;
            int startHeight = (_blocksMaxHeight - item.GetLength(0)) / 2;

            for (int i = 0; i < item.GetLength(0); i++)
            {
                for (int j = 0; j < item.GetLength(1); j++)
                {
                    if (item[i, j])
                    {
                        System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle
                        {
                            Stroke = new SolidColorBrush(Colors.Black),
                            Fill = new SolidColorBrush(Colors.White),
                            Width = partWidth,
                            Height = partHeight
                        };
                        Canvas.SetLeft(rect, (startWidth + j) * partWidth);
                        Canvas.SetTop(rect, (startHeight + i) * partHeight);
                        parts.Add(rect);
                    }
                }
            }

            return parts;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
   
}
