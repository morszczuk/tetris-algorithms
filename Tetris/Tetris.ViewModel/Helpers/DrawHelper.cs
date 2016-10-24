﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris.ViewModel.Helpers
{
    public class DrawHelper
    {
        public static Canvas GetCanvasWidthBrick(Brick brick,int maxWidth,int maxHeight,int canvasWidth,int canvasHeight)
        {
            Canvas canvas=new Canvas();

            canvas.Margin = new Thickness(5, 5, 5, 5);

            int partWidth = (canvasWidth-10)/maxWidth;
            int partHeight = (canvasHeight-10) / maxHeight;

            int startWidth = (maxWidth - brick.Width)/2;
            int startHeight = (maxHeight - brick.Height)/2;

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
    }
}
