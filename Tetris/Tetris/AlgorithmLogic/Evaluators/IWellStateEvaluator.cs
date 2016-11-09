using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    public interface IWellStateEvaluator
    {
        int Evaluate(WellState wellState);
    }
}
