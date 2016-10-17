using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Tetris.Helpers;

namespace Tetris.Windows
{
    /// <summary>
    /// Interaction logic for BricksLibrary.xaml
    /// </summary>
    public partial class BricksLibrary : MetroWindow
    {
        private readonly List<Brick> _bricks;
        private NumericUpDown[] _numerics;

        public BricksLibrary(List<Brick> bricks)
        {
            _bricks = bricks;
            InitializeComponent();
        }

        private void DrawBricks()
        {
            if (_bricks == null) return;
            if (_bricks.Count == 0) return;


            _numerics = new NumericUpDown[_bricks.Count];

            int gridWidth, gridHeight;
            MathHelper.CountGridCellsNo(out gridWidth,out gridHeight,_bricks.Count);

            GenerateGrid(gridWidth,gridHeight);
            int bricksCounter = 0;
            int maxWidth = _bricks.OrderByDescending(e => e.Width).First().Width;
            int maxHeight = _bricks.OrderByDescending(e => e.Height).First().Height;

            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    if (bricksCounter == _bricks.Count) break;
                    Grid cellGrid =new Grid();

                    GenerateCellGrid(cellGrid,bricksCounter);

                    var canvasHeight = ((BricksGrid.ActualHeight/gridHeight)*5)/6;
                   
                    var canvas = DrawHelper.GetCanvasWidthBrick(_bricks[bricksCounter], maxWidth, maxHeight,
                        (int) BricksGrid.ActualWidth/gridWidth, (int)canvasHeight);

                    Grid.SetColumn(cellGrid, j);
                    Grid.SetRow(cellGrid, i);

                    Grid.SetRow(canvas,0);

                    cellGrid.Children.Add(canvas);

                    BricksGrid.Children.Add(cellGrid);

                    bricksCounter++;
                }
            }
        }

        private void GenerateCellGrid(Grid grid,int bricksIndex)
        {
            var row0 = new RowDefinition {Height = new GridLength(5, GridUnitType.Star)};
            grid.RowDefinitions.Add(row0);
            grid.RowDefinitions.Add(new RowDefinition());
            _numerics[bricksIndex] = new NumericUpDown
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Value = _bricks[bricksIndex].Cardinality,
                Minimum = 0
            };

            Grid.SetRow(_numerics[bricksIndex],1);
            grid.Children.Add(_numerics[bricksIndex]);

        }


        private void GenerateGrid(int gridWidth,int gridHeight)
        {
            for (int i = 0; i < gridHeight; i++)
            {
                var row=new RowDefinition();
                BricksGrid.RowDefinitions.Add(row);
            }
            for (int i = 0; i < gridWidth; i++)
            {
                var col = new ColumnDefinition();
                BricksGrid.ColumnDefinitions.Add(col);
            }
        }


        private void BricksLibrary_OnLoaded(object sender, RoutedEventArgs e)
        {
            DrawBricks();
        }

        private void AcceptCardinalities_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _bricks.Count; i++)
            {
                _bricks[i].Cardinality = (int)_numerics[i].Value;
            }
            this.Close();
        }

        private void RejectCardinalities_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
