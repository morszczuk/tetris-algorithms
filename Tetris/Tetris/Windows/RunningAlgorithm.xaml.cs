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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tetris.Helpers;
using Tetris.ViewModel;

namespace Tetris.View.Windows
{
    /// <summary>
    /// Interaction logic for RunningAlgorithm.xaml
    /// </summary>
    public partial class RunningAlgorithm : UserControl
    {
        private static int _isFirst = 1;
        public RunningAlgorithm()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_isFirst == 1)
            {
                _isFirst--;
                return;
            }
            if (!IsVisible) return;
            Reset();
            int wellsNo = ((MainWindowViewModel)this.DataContext).WellNo;
            int wellWidth = ((MainWindowViewModel)this.DataContext).WellWidth;

            int gridpartWidth = (int)(Wells.ActualWidth / wellsNo);

            for (int i = 0; i < wellsNo; i++)
            {

                var col = new ColumnDefinition();
                Wells.ColumnDefinitions.Add(col);

            }

            for (int i = 0; i < wellsNo; i++)
            {
                Grid cellGrid = new Grid
                {
                    Width = gridpartWidth,
                    Height = Wells.ActualHeight
                };

                var canvas = DrawHelper.GetWell(wellWidth, wellWidth, gridpartWidth, (int)Wells.ActualHeight);


                Grid.SetColumn(cellGrid, i);

                cellGrid.Children.Add(canvas);

                Wells.Children.Add(cellGrid);

            }
        }
        private void Reset()
        {
            Wells.Children.Clear();
        }
    }
}
