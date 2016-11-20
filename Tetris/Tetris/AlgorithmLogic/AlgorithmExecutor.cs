using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.Models;

namespace Tetris.AlgorithmLogic
{
    public class AlgorithmExecutor
    {
        public List<WellState> ActiveStates { get; private set; }
        public AlgorithmInput Settings { get; }
        public bool IsStopped { get; private set; }

        private readonly IWellStateEvaluator _evaluator;
        private readonly StatesGenerator _statesGenerator;

        public AlgorithmExecutor(AlgorithmInput settings)
        {
            Settings = settings;
            _evaluator = new ColumnFillEvaluator();
            IBrickPositioner positioner = new BasicBottomLeftPositioner();
            _statesGenerator = new StatesGenerator(positioner);

            InitializeActiveStates();
        }

        public AlgorithmExecutor(AlgorithmInput settings, IWellStateEvaluator evaluator, IBrickPositioner positioner)
        {
            Settings = settings;
            _evaluator = evaluator;
            _statesGenerator = new StatesGenerator(positioner);
            InitializeActiveStates();
        }

        public void Reset()
        {
            InitializeActiveStates();
        }

        private void InitializeActiveStates()
        {
            var well = new Well(Settings.WellWidth);
            var initialState = new WellState(well, Settings.BricksShelf);
            ActiveStates = new List<WellState>() {initialState};
        }

        public void Start()
        {
            IsStopped = false;
            while (!IsStopped && !IsFinished())
            {
                MakeStep();
            }
        }

        public void Stop()
        {
            IsStopped = true;
        }

        public void MakeStep()
        {
            ActiveStates = ActiveStates
                            .AsParallel()
                            .SelectMany(_statesGenerator.Generate)
                            .OrderByDescending(_evaluator.Evaluate)
                            .Take(Settings.WellNo).ToList();
        }

        public bool IsFinished()
        {
            return ActiveStates.All(s => !s.BricksShelf.AvailableBrickTypes.Any());
        }

    }
}
