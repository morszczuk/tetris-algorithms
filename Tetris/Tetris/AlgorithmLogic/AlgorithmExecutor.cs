using System.Collections.Generic;
using System.Linq;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.AlgorithmLogic.Strategies;
using Tetris.Models;

namespace Tetris.AlgorithmLogic
{
    public class AlgorithmExecutor
    {
        public AlgorithmInput Settings { get; }
        public List<WellState> ActiveStates { get; private set; }

        public AlgorithmExecutor(AlgorithmInput settings)
        {
            Settings = settings;
        }

        public void Run()
        {
            var well = new Well(Settings.WellWidth);
            var initialState = new WellState(well, Settings.BricksShelf);

            ActiveStates = new List<WellState>();
            for (var i = 0; i < Settings.WellNo; i++)
                ActiveStates.Add(initialState);

            IBrickPositioner positioner = new BasicBottomLeftPositioner();
            IWellStateEvaluator evaluator = new FillEvaluator();
            IWellStateSelectionStrategy selectionStrategy = new TopKStates(Settings.WellNo, evaluator);

            while (ActiveStates.Any(s => s.BricksShelf.AvailableBricks().Any()))
            {
                var newStates = new List<WellState>();
                foreach (var state in ActiveStates)
                {
                    foreach (var brick in state.BricksShelf.AvailableBricks())
                    {
                        var tmpState = new WellState(state);
                        tmpState.BricksShelf.Bricks[brick]--;

                        newStates.AddRange(positioner.PlaceBrick(tmpState, brick));
                    }
                }
                ActiveStates = selectionStrategy.Select(newStates);
            }
        }

    }
}
