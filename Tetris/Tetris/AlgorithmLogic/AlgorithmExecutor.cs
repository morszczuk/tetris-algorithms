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
        /// <summary>
        /// List of k WellStates in the current algorithm step
        /// </summary>
        public List<WellState> ActiveStates { get; private set; }

        /// <summary>
        /// AlgorithmInput object passed in constructor as algorithm initialization settings
        /// </summary>
        public AlgorithmInput Settings { get; }

        /// <summary>
        /// Marks if continuous algorithm calculations are stopped
        /// </summary>
        public bool IsStopped { get; private set; }

        private readonly IWellStateEvaluator _evaluator;
        private readonly StatesGenerator _statesGenerator;

        public AlgorithmExecutor(AlgorithmInput settings)
        {
            Settings = settings;
            _evaluator = (IWellStateEvaluator)Activator.CreateInstance(Settings.EvaluatorType);
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

        public AlgorithmExecutor(AlgorithmInput settings, List<WellState> activeStates) : this(settings)
        {
            Settings = settings;
            _evaluator = (IWellStateEvaluator)Activator.CreateInstance(Settings.EvaluatorType);
            IBrickPositioner positioner = new BasicBottomLeftPositioner();
            _statesGenerator = new StatesGenerator(positioner);

            InitializeActiveStates();
            ActiveStates = activeStates;
        }

        /// <summary>
        /// Resets algorithm state
        /// </summary>
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

        /// <summary>
        /// Starts continuous calculations
        /// </summary>
        public void Start()
        {
            IsStopped = false;
            while (!IsStopped && !IsFinished())
            {
                MakeStep();
            }
        }

        /// <summary>
        /// Stops continuous calculations
        /// </summary>
        public void Stop()
        {
            IsStopped = true;
        }

        /// <summary>
        /// Makes one step of the algorithm
        /// </summary>
        public void MakeStep()
        {
            ActiveStates = ActiveStates
                            .AsParallel()
                            .SelectMany(_statesGenerator.Generate)
                            .OrderByDescending(_evaluator.Evaluate)
                            .Take(Settings.WellNo).ToList();
        }

        /// <summary>
        /// Checks if algorithm is finished
        /// </summary>
        /// <returns>true if algorithm is finshed</returns>
        public bool IsFinished()
        {
            return ActiveStates.All(s => !s.BricksShelf.AvailableBrickTypes.Any());
        }

    }
}
