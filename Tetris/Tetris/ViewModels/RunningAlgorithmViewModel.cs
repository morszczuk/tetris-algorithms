using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Tetris.AlgorithmLogic;
using Tetris.Enums;
using Tetris.Models;
using System.Threading.Tasks;
using Microsoft.Win32;
using Tetris.Helpers;
using System.Linq;

namespace Tetris.ViewModels
{
    /// <summary>
    /// Simulation view model
    /// </summary>
    public class RunningAlgorithmViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private readonly MainWindowViewModel _mainWindowViewModel;

        private readonly AlgorithmInput _algorithmParameters;
        private readonly AlgorithmExecutor _executor;
        private Task _task;


        private bool _areComputationsPaused;
        private bool _areComputationsFinished;
        private List<BrickType> _brickTypes;
        private bool _areComputationsStarted;
        private IEnumerable<WellState> _activeStates;


        #region BindingProperties

        public IEnumerable<WellState> ActiveStates
        {
            get { return _activeStates; }
            private set
            {
                _activeStates = value;
                NotifyOfPropertyChange(() => ActiveStates);
            }
        }

        public bool AreComputationsStarted
        {
            get { return _areComputationsStarted; }
            set
            {
                _areComputationsStarted = value;
                NotifyOfPropertyChange(() => AreComputationsStarted);
                NotifyOfPropertyChange(() => ComputationsButtonName);
            }
        }

        public bool AreComputationsPaused
        {
            get { return _areComputationsPaused; }
            set
            {
                Console.WriteLine(value ? "Pausing..." : "Resuming...");
                _areComputationsPaused = value;
                NotifyOfPropertyChange(() => AreComputationsPaused);
                NotifyOfPropertyChange(() => ComputationsButtonName);

                if (_areComputationsPaused)
                {
                    StopComputations();
                }
                else
                {
                    StartComputations();
                }
            }
        }


        public bool AreComputationsFinished
        {
            get { return _areComputationsFinished; }
            private set
            {
                _areComputationsFinished = value;
                NotifyOfPropertyChange(() => AreComputationsFinished);
                NotifyOfPropertyChange(() => ComputationsButtonName);
            }
        }

        public string ComputationsButtonName
        {
            get
            {
                if (AreComputationsFinished) return "TimerCheck";
                if (!AreComputationsStarted) return "TimerPlay";
                if (AreComputationsPaused) return "TimerResume;";
                return "TimerPause";
            }
        }

        public bool IsStep { get; }
        public int StepCount { get; set; } = 1;

        public override string DisplayName { get; set; } = "Uruchomiony algorytm";

        #endregion

        public RunningAlgorithmViewModel(IWindowManager windowManager,
            IEnumerable<BrickType> bricks,
            MainWindowViewModel mainWindowViewModel,
            bool isStep, int wellNo, int wellWidth,
            Type evaluator)
        {
            _windowManager = windowManager;
            _mainWindowViewModel = mainWindowViewModel;
            IsStep = isStep;
            _algorithmParameters = new AlgorithmInput(new BricksShelf(bricks),
                wellNo,
                wellWidth,
                isStep ? AlgorithmsEnum.Step : AlgorithmsEnum.Continuous,
                evaluator);
            _executor = new AlgorithmExecutor(_algorithmParameters);

            _areComputationsStarted = false;
            _areComputationsPaused = false;
            _areComputationsFinished = false;
        }

        public RunningAlgorithmViewModel(IWindowManager windowManager, MainWindowViewModel mainWindowViewModel, AlgorithmInput settings, List<WellState> activeStates)
        {
            _windowManager = windowManager;
            _mainWindowViewModel = mainWindowViewModel;
            _algorithmParameters = settings;
            _executor = new AlgorithmExecutor(_algorithmParameters, activeStates);
            _areComputationsStarted = true;
            _areComputationsPaused = true;
            _areComputationsFinished = false;
            ActiveStates = activeStates;
        }

        public RunningAlgorithmViewModel(IWindowManager _windowManager, List<BrickType> brickTypes)
        {
            this._windowManager = _windowManager;
            this._brickTypes = brickTypes;
        }




        #region ButtonClicks

        public void ToggleRunnningAlgorithmOnClick()
        {
            if (AreComputationsFinished) return;
            if (!AreComputationsStarted)
            {
                AreComputationsStarted = true;
                AreComputationsPaused = false;
            }
            else
            {
                AreComputationsPaused = !AreComputationsPaused;
            }
        }

        public void StartComputations()
        {
            _task = new Task(() => _executor.Start());
            _task.ContinueWith((t) =>
            {
                AreComputationsFinished = _executor.IsFinished();
                ActiveStates = _executor.ActiveStates;
            });
            _task.Start();
        }

        public void StopComputations()
        {
            _executor.Stop();
        }

        public void MakeStepOnClick()
        {
            if (AreComputationsFinished) return;
            for (var i = 0; i < StepCount; i++)
            {
                _executor.MakeStep();
                if (_executor.IsFinished())
                {
                    AreComputationsFinished = true;
                    break;
                }
            }
            ActiveStates = _executor.ActiveStates;
        }

        public void EndComputationOnClick()
        {
            _mainWindowViewModel.ActivatgeShellView();
        }

        public void SaveAlgorithmStateOnClick()
        {
            var saveFileDialog = new SaveFileDialog { Filter = "Algorithm state file (*.astate)|*.astate" };
            if (saveFileDialog.ShowDialog() != true) return;
            var algorithmState = new Tuple<List<WellState>, AlgorithmInput>(ActiveStates.ToList(), _executor.Settings);
            BinarySerializer.WriteToBinaryFile<Tuple<List<WellState>, AlgorithmInput>>(saveFileDialog.FileName, algorithmState);
        }
        #endregion
    }
}
