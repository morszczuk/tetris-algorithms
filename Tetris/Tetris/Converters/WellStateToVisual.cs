using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tetris.Models;
using Color = System.Drawing.Color;
using Image = System.Windows.Controls.Image;

namespace Tetris.Converters
{
    /// <summary>
    /// Converter to generate graphic representation of well - canvas
    /// </summary>
    class WellStateToVisual : IValueConverter
    {
        /// <summary>
        /// Difference between colors of two bricks in RGB
        /// </summary>
        private readonly int _colorStep = 30;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            var state = value as WellState;
            if (state == null) return null;

            var bitmap = new Bitmap(state.Well.Width, state.Fill.Count);
            for (var y = 0; y < bitmap.Height; y++)
                for (var x = 0; x < bitmap.Width; x++)
                    bitmap.SetPixel(x, y, Color.LightBlue);

            var colorCounter = 0;
            for (var i = 0; i < state.Bricks.Count; i++)
            {
                var brickPos = state.Bricks[i];
                var brick = brickPos.Brick;
                if (_colorStep*colorCounter > 255) colorCounter -= i;
                var c = (byte) (_colorStep*colorCounter);
                var color = i < state.Bricks.Count - 1
                    ? Color.FromArgb(255, c, c, c)
                    : Color.FromArgb(255, 255, 255, 102);

                for (var j = 0; j < brick.Height; j++)
                {
                    for (var k = 0; k < brick.Width; k++)
                    {
                        if (brick.Body[j, k])
                        {
                            var y = bitmap.Height - brickPos.Y - brick.Height + j;
                            var x = brickPos.X + k;
                            bitmap.SetPixel(x, y, color);
                        }
                    }
                }
                colorCounter++;
            }

            var bitmapImage = new BitmapImage();
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
