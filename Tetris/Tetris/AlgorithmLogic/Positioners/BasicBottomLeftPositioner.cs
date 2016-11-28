using System.Collections.Generic;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Positioners
{
    /// <summary>
    /// Allways tries to position brick at the most bottom and left position
    /// </summary>
    public class BasicBottomLeftPositioner : IBrickPositioner
    {
        public IEnumerable<WellState> PlaceBrick(WellState wellState, Brick brick)
        {
            for (var y = wellState.FullRows; y <= wellState.Fill.Count; y++)
            {
                for (var x = 0; x <= (wellState.Well.Width - brick.Width); x++)
                {
                    if (wellState.AddBrick(brick, x, y))
                    {
                        return new List<WellState>() { wellState };
                    }
                }
            }

            return null;
        }
    }
}
