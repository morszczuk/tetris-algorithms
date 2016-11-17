using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.AlgorithmLogic.Strategies;
using Tetris.Models;

namespace Tetris.AlgorithmLogic
{
    class AlgorithmExecutor
    {
        public AlgorithmInput Settings { get; private set; }

        public AlgorithmExecutor(AlgorithmInput settings)
        {
            Settings = settings;
        }

        public void Run()
        {
            var well = new Well(Settings.WellWidth);
            var initialState = new WellState(well, Settings.BricksShelf);
            var activeStates = new List<WellState>() { initialState };

            IBrickPositioner positioner = null;
            IWellStateEvaluator evaluator = null;
            IWellStateSelectionStrategy selectionStrategy = null;

            while (activeStates.Any(s => s.BricksShelf.AvailableBricks().Any()))
            {
                
            }
        }

    }
}
