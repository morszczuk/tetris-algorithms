using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.AlgorithmLogic.Strategies;
using Tetris.Models;

namespace Tetris.AlgorithmLogic
{
    public class AlgorithmExecutor
    {
        public List<WellState> ActiveStates { get; private set; }
        
        private readonly IWellStateSelectionStrategy _selectionStrategy;
        private readonly StatesGenerator _statesGenerator;

        public AlgorithmExecutor(AlgorithmInput settings)
        {
            IWellStateEvaluator evaluator = new ColumnFillEvaluator();
            IBrickPositioner positioner = new BasicBottomLeftPositioner();

            _selectionStrategy = new TopKStates(settings.WellNo, evaluator);
            _statesGenerator = new StatesGenerator(positioner);

            InitializeActiveStates(settings);
        }

        private void InitializeActiveStates(AlgorithmInput settings)
        {
            var well = new Well(settings.WellWidth);
            var initialState = new WellState(well, settings.BricksShelf);
            ActiveStates = new List<WellState>();
            for (var i = 0; i < settings.WellNo; i++)
                ActiveStates.Add(initialState);
        }

        public void Run()
        {
            while (!IsFinished())
            {
                MakeStep();
            }
        }

        public void MakeStep()
        {
            var generatedStates = new List<WellState>();
            foreach (var state in ActiveStates)
            {
                generatedStates.AddRange(_statesGenerator.Generate(state));
            }
            ActiveStates = _selectionStrategy.Select(generatedStates);
        }

        public bool IsFinished()
        {
            return ActiveStates.All(s => !s.BricksShelf.AvailableBricks().Any());
        }

    }
}
