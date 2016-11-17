using System.Collections.Generic;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Strategies
{
    interface IWellStateSelectionStrategy
    {
        List<WellState> Select(List<WellState> wellStates);
    }
}
