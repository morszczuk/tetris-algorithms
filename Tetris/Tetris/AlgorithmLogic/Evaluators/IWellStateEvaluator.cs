using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    /// <summary>
    /// Interface for well state comparision
    /// </summary>
    public interface IWellStateEvaluator
    {
        int Evaluate(WellState wellState);
    }
}
