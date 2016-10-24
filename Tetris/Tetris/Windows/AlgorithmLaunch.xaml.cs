using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using Tetris.Helpers;
using Tetris.Windows;
using Tetris.ViewModel;

namespace Tetris.View.Windows
{
    /// <summary>
    /// Interaction logic for AlgorithmLaunch.xaml
    /// </summary>
    public partial class AlgorithmLaunch : UserControl
    {
        private List<Brick> _bricks;
        private int _wellWidth;

        public AlgorithmLaunch()
        {
            InitializeComponent();
        }

        private void LoadBricks_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var loader =new BricksLoader(new StreamReader(openFileDialog.FileName));
                var result=loader.ReadFile();
                _bricks = result.Bricks;
                _wellWidth = result.WellWidth;
                WellWidthNumericUpDown.Value = _wellWidth;
                LibraryButton.Visibility = Visibility.Visible;
                LibraryText.Visibility = Visibility.Visible;
            }

        }

        private void BrowseBricks_OnClick(object sender, RoutedEventArgs e)
        {
            var window=new BricksLibrary(_bricks);
            window.ShowDialog();
        }
    }
}
