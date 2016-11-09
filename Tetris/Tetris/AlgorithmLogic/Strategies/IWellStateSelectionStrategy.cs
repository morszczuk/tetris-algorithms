using System.Collections.Generic;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Strategies
{
    interface IWellStateSelectionStrategy
    {
        IEnumerable<WellState> Select(IEnumerable<WellState> wellStates);
    }
}
