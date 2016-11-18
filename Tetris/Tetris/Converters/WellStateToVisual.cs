using System;
using System.Collections.Generic;
using System.Drawing;
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
using Color = System.Windows.Media.Color;
using System.Drawing;

namespace Tetris.Converters
{
    class WellStateToVisual : IValueConverter
    {

        private readonly int _colorStep = 10;
        

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var item = value as WellState;

            var buttons = new List<Button>();
            int colorCounter = 0;

            for (int i = 0; i < item.Bricks.Count; i++)
            {

                var brick = item.Bricks[i].Brick;
                if (_colorStep * colorCounter > 255) colorCounter -= i;
                byte c = (byte)(_colorStep * colorCounter);
                var colour = Color.FromArgb(255, c, c, c);
                for (int j = 0; j < brick.Height; j++)
                {
                    for (int k = 0; k < brick.Width; k++)
                    {
                        if (brick.Body[j, k])
                        {
                            Button b = new Button()
                            {
                                Background = new SolidColorBrush(colour)
                            };
                            Grid.SetRow(b, item.Fill.Count - 1 - item.Bricks[i].Y + j);
                            Grid.SetColumn(b, item.Bricks[i].X + k);
                            buttons.Add(b);
                        }   
                    }
                }
                colorCounter++;
            }
            return buttons;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
