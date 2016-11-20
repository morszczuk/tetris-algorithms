using System.Collections.Generic;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Positioners
{
    public interface IBrickPositioner
    {
        IEnumerable<WellState> PlaceBrick(WellState wellState, BrickType brick);
    }
}
