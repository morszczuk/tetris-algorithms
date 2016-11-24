using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Tetris.Models;

namespace Tetris.Converters
{
    /// <summary>
    /// Converts Brick as Canvas
    /// </summary>
    class BlockToVisual : IValueConverter
    {
        /// <summary>
        /// consts used for better visual experience - normalize size of every canvas drawn
        /// </summary>
        private const int _blocksMaxHeight = 8;
        private const int _blocksMaxWidth = 8;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = ((Brick)value).Body;

            var parts = new List<System.Windows.Shapes.Rectangle>();

            int blockWidth = Math.Max(_blocksMaxWidth, item.GetLength(1));
            int blockHeight = Math.Max(_blocksMaxHeight, item.GetLength(0));


            var partWidth = (200) / blockWidth;
            var partHeight = (200) / blockHeight;

            var startWidth = (blockWidth - item.GetLength(1)) / 2;
            var startHeight = (blockHeight - item.GetLength(0)) / 2;

            for (var i = 0; i < item.GetLength(0); i++)
            {
                for (var j = 0; j < item.GetLength(1); j++)
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
