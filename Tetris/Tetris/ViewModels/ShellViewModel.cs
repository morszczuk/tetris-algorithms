using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
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
        private int _wellNo = 2;
        private int _wellWidth = 10;
        private List<BrickType> _brickTypes;
        private bool _libraryIsVisible = false;
        private MainWindowViewModel _mainWindow;


        public ShellViewModel(IWindowManager windowManager, MainWindowViewModel mainWindow)
        {
            _windowManager = windowManager;
            _mainWindow = mainWindow;
        }

        public override string DisplayName { get; set; } = "Tetris";

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

        public List<BrickType> BrickTypes
        {
            get
            {
                return _brickTypes;    
            }
            set
            {
                _brickTypes = value;
                NotifyOfPropertyChange(() => BrickTypes);
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
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true) return;
            var loader = new BricksLoader(new StreamReader(openFileDialog.FileName));
            var result = loader.ReadFile();
            _brickTypes = result.BrickTypes;
            WellWidth = result.WellWidth;
            LibraryIsVisible = true;
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
