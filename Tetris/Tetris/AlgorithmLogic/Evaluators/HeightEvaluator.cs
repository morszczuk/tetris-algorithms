using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    public class HeightEvaluator : IWellStateEvaluator
    {
        public HeightEvaluator() {}

        public int Evaluate(WellState wellState)
        {
            return -wellState.Fill.Count;
        }
    }
}
