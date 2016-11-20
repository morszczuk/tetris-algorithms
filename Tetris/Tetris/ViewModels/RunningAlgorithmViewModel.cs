﻿using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Tetris.AlgorithmLogic;
using Tetris.Enums;
using Tetris.Models;
using System.Threading.Tasks;

namespace Tetris.ViewModels
{

    public class RunningAlgorithmViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private MainWindowViewModel _mainWindowViewModel;

        private readonly AlgorithmInput _algorithmParameters;
        private AlgorithmExecutor _executor;
        private Task _task;

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

        private bool _areComputationsStarted;
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

        private bool _areComputationsPaused;
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

        private bool _areComputationsFinished;
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

        public RunningAlgorithmViewModel(IWindowManager windowManager, IEnumerable<BrickType> bricks, MainWindowViewModel mainWindowViewModel, bool isStep, int wellNo, int wellWidth)
        {
            _windowManager = windowManager;
            _mainWindowViewModel = mainWindowViewModel;
            IsStep = isStep;
            _algorithmParameters = new AlgorithmInput(new BricksShelf(bricks), wellNo, wellWidth, isStep ? AlgorithmsEnum.Step : AlgorithmsEnum.Continuous);
            _executor = new AlgorithmExecutor(_algorithmParameters);

            _areComputationsStarted = false;
            _areComputationsPaused = false;
            _areComputationsFinished = false;
        }

        public override string DisplayName { get; set; } = "Uruchomiony algorytm";

        public void ToggleRunnningAlgorithmOnClick()
        {
            if (AreComputationsFinished) return;
            if (!AreComputationsStarted)
            {
                AreComputationsStarted = true;
                AreComputationsPaused = false;
            } else
            {
                AreComputationsPaused = !AreComputationsPaused;
            }
        }

        public void StartComputations()
        {
            _task = new Task(() => _executor.Start());
            _task.ContinueWith((t) => {
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
                if (_executor.IsFinished())
                {
                    AreComputationsFinished = true;
                    break;
                }
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
