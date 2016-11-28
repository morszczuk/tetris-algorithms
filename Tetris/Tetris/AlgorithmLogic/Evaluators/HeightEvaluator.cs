using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    /// <summary>
    /// Evaluator which compare states by number of rows with bricks
    /// </summary>
    public class HeightEvaluator : IWellStateEvaluator
    {
        public int Evaluate(WellState wellState)
        {
            return -wellState.Fill.Count;
        }
    }
}
