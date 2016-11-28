using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    /// <summary>
    /// Evaluator that calculates the global height of the well and 
    /// combines it with the height of each column
    /// </summary>
    public class ColumnFillEvaluator : IWellStateEvaluator
    {
        public int Evaluate(WellState wellState)
        {
            var value = wellState.Well.Width * wellState.Fill.Count;
            for (var x = 0; x < wellState.Well.Width; x++)
            {
                for (var y = wellState.Fill.Count - 1; y >= 0; y--)
                {
                    if (wellState.IsFilled(x, y))
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