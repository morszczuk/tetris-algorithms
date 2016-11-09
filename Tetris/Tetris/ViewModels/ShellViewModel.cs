using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Win32;
using Tetris.Helpers;
using Tetris.Models;

namespace Tetris.ViewModels
{
    [Export(typeof(ShellViewModel))]

    public class ShellViewModel :  Conductor<object>, IHaveDisplayName
    {
        private readonly IWindowManager _windowManager;
        private string _displayName = "Tetris";
        private int _wellNo = 2;
        private int _wellWidth = 10;
        private List<Brick> _bricks;
        private bool _libraryIsVisible = false;
        private MainWindowViewModel _mainWindow;


        public ShellViewModel(IWindowManager windowManager, MainWindowViewModel mainWindow)
        {
            _windowManager = windowManager;
            _mainWindow = mainWindow;
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public int WellWidth
        {
            get
            {
                return _wellWidth;
            }
            set
            {
                _wellWidth = value;
                NotifyOfPropertyChange(() => WellWidth);
            }
        }

        public int WellNo
        {
            get
            {
                return _wellNo;
            }
            set
            {
                _wellNo = value;
                NotifyOfPropertyChange(() => WellNo);
            }
        }

        public List<Brick> Bricks
        {
            get { return _bricks;
                
            }
            set
            {
                _bricks = value;
                NotifyOfPropertyChange(() => Bricks);
            }
        }

        public bool LibraryIsVisible
        {
            get { return _libraryIsVisible; }
            set
            {
                _libraryIsVisible = value;
                NotifyOfPropertyChange(() => LibraryIsVisible);
            }
        }

        public void ReadBricks()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var loader = new BricksLoader(new StreamReader(openFileDialog.FileName));
                var result = loader.ReadFile();
                _bricks = result.Bricks;
                WellWidth = result.WellWidth;
                LibraryIsVisible = true;
            }
        }
        public void BricksLibrary()
        {
            _mainWindow.ActivateLibrary();
        }

        public void GoAlgorithmByStep()
        {
            _mainWindow.ActivateRunningAlgorithmView(true);
        }

        public void GoAlgorithm()
        {
            _mainWindow.ActivateRunningAlgorithmView(false);
        }

    }
}
