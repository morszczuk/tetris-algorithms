using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    public class ColumnFillEvaluator : IWellStateEvaluator
    {
        public int Evaluate(WellState wellState)
        {
            var value = wellState.Well.Width * wellState.Fill.Count;
            for (var x = 0; x < wellState.Well.Width; x++)
            {
                for (var y = wellState.Fill.Count - 1; y >= 0; y--)
                {
                    if (wellState.Fill[y][x])
                    {
                        value += y;
                        break;
                    }
                }
            }
            return -value;
        }
    }
}