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
using Tetris.Dialogs;
using Tetris.ViewModel;
using Tetris.ViewModel.Helpers;

namespace Tetris.View.Windows
{
    /// <summary>
    /// Interaction logic for AlgorithmLaunch.xaml
    /// </summary>
    public partial class AlgorithmLaunch : UserControl
    {
        public AlgorithmLaunch()
        {
            InitializeComponent();
        }
    }
}
