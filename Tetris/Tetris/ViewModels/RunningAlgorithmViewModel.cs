using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Tetris.AlgorithmLogic;
using Tetris.Enums;
using Tetris.Models;

namespace Tetris.ViewModels
{

    public class RunningAlgorithmViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private MainWindowViewModel _mainWindowViewModel;

        private readonly AlgorithmInput _algorithmParameters;
        private AlgorithmExecutor _executor;
        private IEnumerable<WellState> _activeStates;

        public IEnumerable<WellState> ActiveStates
        {
            get { return _activeStates; }
            private set
            {
                _activeStates = value;
                NotifyOfPropertyChange(() => ActiveStates);
            }
        }

        private bool _areComputationsRunning;

        public bool AreComputationsRunning
        {
            get
            {
                return _areComputationsRunning;
            }
            set
            {
                _areComputationsRunning = value;
                NotifyOfPropertyChange(() => AreComputationsRunning);
                NotifyOfPropertyChange(() => ComputationsButtonName);
            }
        }

        public string ComputationsButtonName => AreComputationsRunning ? "TimerPause" : "TimerPlay";


        public bool IsStep { get; }
        public int StepCount { get; set; } = 1;

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
        }

        public override string DisplayName { get; set; } = "Uruchomiony algorytm";

        public void ToggleRunnningAlgorithmOnClick()
        {
            AreComputationsRunning = !AreComputationsRunning;
            if (AreComputationsRunning)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                _executor.Run();
                watch.Stop();
                Console.WriteLine("Time: " + watch.ElapsedMilliseconds);
                ActiveStates = _executor.ActiveStates;
            }
            else
            {
                _executor.Reset();
            }
        }

        public void MakeStepOnClick()
        {
            for (var i = 0; i < StepCount; i++)
            {
                if (_executor.IsFinished()) break;
                _executor.MakeStep();
            }
            ActiveStates = _executor.ActiveStates;
        }

        public void EndComputationOnClick()
        {
            _mainWindowViewModel.ActivatgeShellView();
        }
    }
}
