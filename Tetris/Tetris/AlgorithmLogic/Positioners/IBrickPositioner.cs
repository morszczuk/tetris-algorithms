using System.Collections.Generic;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Positioners
{
    /// <summary>
    /// Interface for brick new brick posisioning
    /// </summary>
    public interface IBrickPositioner
    {
        IEnumerable<WellState> PlaceBrick(WellState wellState, Brick brick);
    }
}
