using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Tetris.AlgorithmLogic;
using Tetris.Enums;
using Tetris.Models;

namespace Tetris.ViewModels
{

    public class RunningAlgorithmViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private string _displayName = "Uruchomiony algorytm";
        private MainWindowViewModel _mainWindowViewModel;

        private AlgorithmInput _algorithmParameters;
        private AlgorithmExecutor _executor;

        public AlgorithmExecutor Executor
        {
            get { return _executor; }
            set
            {
                _executor = value;
                NotifyOfPropertyChange(() => Executor);
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

        public RunningAlgorithmViewModel(IWindowManager windowManager, List<Brick> bricks, MainWindowViewModel mainWindowViewModel, bool isStep, int wellNo, int wellWidth)
        {
            _windowManager = windowManager;
            _mainWindowViewModel = mainWindowViewModel;
            IsStep = isStep;
            _algorithmParameters = new AlgorithmInput(new BricksShelf(bricks), wellNo, wellWidth, isStep ? AlgorithmsEnum.Step : AlgorithmsEnum.Continuous);

        }

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }




        public void PlayPauseOnClick()
        {
            AreComputationsRunning = !AreComputationsRunning;

            Executor= new AlgorithmExecutor(_algorithmParameters);

            var executor = new AlgorithmExecutor(_algorithmParameters);
            executor.Run();

            Executor = executor;

        }

        public void EndComputationOnClick()
        {
            _mainWindowViewModel.ActivatgeShellView();
        }
    }
}
