using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.Helpers;
using Tetris.Models;
using Tetris.AlgorithmLogic;

namespace Tetris.ViewModels
{
    /// <summary>
    /// Start view model
    /// </summary>
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : Conductor<object>, IHaveDisplayName
    {
        private const string _errorTitle = "Error";
        private const string _incorrectFile = "Niewłaściwy format pliku";


        private readonly IWindowManager _windowManager;
        private int _wellNo = 2;
        private int _wellWidth = 10;
        private List<BrickType> _brickTypes;
        private bool _libraryIsVisible = false;
        private readonly MainWindowViewModel _mainWindow;
        private Type _selectedEvaluator;


        public ShellViewModel(IWindowManager windowManager, MainWindowViewModel mainWindow)
        {
            _windowManager = windowManager;
            _mainWindow = mainWindow;

            SelectedEvaluator = Evaluators[0];
        }

        #region BindingProperties

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

        public List<Type> Evaluators
        {
            get
            {
                var type = typeof(IWellStateEvaluator);
                var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p)).ToList();

                types.Remove(typeof(IWellStateEvaluator));
                types.Remove(typeof(FillWithoutTopNEvaluator));
                return types;
            }
        }

        public Type SelectedEvaluator
        {
            get
            {
                return _selectedEvaluator;
            }
            set
            {
                _selectedEvaluator = value;
                NotifyOfPropertyChange(() => SelectedEvaluator);
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


        #endregion


        #region ButtonClicks

        public void ReadBricks()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true) return;
            try
            {
                var loader = new BricksLoader(new StreamReader(openFileDialog.FileName));
                var result = loader.ReadFile();
                _brickTypes = result.BrickTypes;
                WellWidth = result.WellWidth;
                LibraryIsVisible = true;
            }
            catch (Exception)
            {
                Tetris.Helpers.MessageBox.ShowMessage(Application.Current.MainWindow, _errorTitle,
                    _incorrectFile, MessageDialogStyle.Affirmative);

            }
        }

        public void BricksLibrary()
        {
            _mainWindow.ActivateLibrary();
        }

        public void GoAlgorithmByStep()
        {
            _mainWindow.ActivateRunningAlgorithmView(true, SelectedEvaluator);
        }

        public void GoAlgorithm()
        {
            _mainWindow.ActivateRunningAlgorithmView(false, SelectedEvaluator);
        }

        public void LoadAlgorithmStateOnClick()
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Algorithm state file (*.astate)|*.astate";
                if (openFileDialog.ShowDialog() != true) return;
                var algorithmState = BinarySerializer.ReadFromBinaryFile<Tuple<List<WellState>, AlgorithmInput>>(openFileDialog.FileName);
                BrickTypes = BrickType.GetBrickTypes(algorithmState.Item2.BricksShelf).ToList();
                LibraryIsVisible = true;
                var item = new RunningAlgorithmViewModel(_windowManager, _mainWindow, algorithmState.Item2, algorithmState.Item1);
                _mainWindow.ActivateItem(item);
            }
            catch
            {
                Tetris.Helpers.MessageBox.ShowMessage(Application.Current.MainWindow, _errorTitle,
   _incorrectFile, MessageDialogStyle.Affirmative);
            }

        }

        #endregion
    }
}
