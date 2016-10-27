using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris.Helpers
{
    public class DrawHelper
    {
        public static Canvas GetCanvasWidthBrick(Brick brick, int maxWidth, int maxHeight, int canvasWidth, int canvasHeight)
        {
            Canvas canvas = new Canvas();

            canvas.Margin = new Thickness(5, 5, 5, 5);

            int partWidth = (canvasWidth - 10) / maxWidth;
            int partHeight = (canvasHeight - 10) / maxHeight;

            int startWidth = (maxWidth - brick.Width) / 2;
            int startHeight = (maxHeight - brick.Height) / 2;

            for (int i = 0; i < brick.Height; i++)
            {
                for (int j = 0; j < brick.Width; j++)
                {
                    if (brick.Body[i, j] == 1)
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
                        canvas.Children.Add(rect);
                    }
                }
            }

            return canvas;
        }

        public static Canvas GetWell(int maxWidth, int maxHeight, int canvasWidth, int canvasHeight)
        {
            Canvas canvas = new Canvas();

            canvas.Margin = new Thickness(5, 5, 5, 5);

            int partWidth = (canvasWidth - 10) / maxWidth;
            int partHeight = (canvasHeight - 10) / maxHeight;


            for (int i = 0; i < maxHeight; i++)
            {
                for (int j = 0; j < maxWidth; j++)
                {

                    System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle
                    {
                        Stroke = new SolidColorBrush(Colors.Black),
                        Fill = new SolidColorBrush(Colors.White),
                        Width = partWidth,
                        Height = partHeight
                    };
                    Canvas.SetLeft(rect, (j) * partWidth);
                    Canvas.SetTop(rect, (i) * partHeight);
                    canvas.Children.Add(rect);

                }
            }

            return canvas;
        }

    }
}
