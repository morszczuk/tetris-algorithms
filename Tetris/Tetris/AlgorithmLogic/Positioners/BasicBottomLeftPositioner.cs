using System.Collections.Generic;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Positioners
{
    public class BasicBottomLeftPositioner : IBrickPositioner
    {
        public IEnumerable<WellState> PlaceBrick(WellState wellState, Brick brick)
        {
            for (var y = wellState.FullRows; y <= wellState.Fill.Count; y++)
            {
                for (var x = 0; x < wellState.Well.Width; x++)
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
