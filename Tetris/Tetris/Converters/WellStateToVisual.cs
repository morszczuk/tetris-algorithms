using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Tetris.Models;
using Color = System.Windows.Media.Color;

namespace Tetris.Converters
{
    class WellStateToVisual : IValueConverter
    {

        private readonly int _colorStep = 30;
        

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
                var color = i < item.Bricks.Count - 1 ? Color.FromArgb(255, c, c, c) : Color.FromArgb(255, 255, 255, 102);
                for (int j = 0; j < brick.Height; j++)
                {
                    for (int k = 0; k < brick.Width; k++)
                    {
                        if (brick.Body[j, k])
                        {
                            Button b = new Button()
                            {
                                Background = new SolidColorBrush(color)
                            };

                            Binding binding = new Binding()
                            {
                                Path = new PropertyPath("ActualWidth"),
                                Source = b
                            };
                            BindingOperations.SetBinding(b, Button.HeightProperty, binding);

                            var bottomRow = item.Fill.Count - 1 - item.Bricks[i].Y;
                            Grid.SetRow(b, bottomRow - (brick.Height - 1) + j);
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
