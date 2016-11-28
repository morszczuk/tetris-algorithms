using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    /// <summary>
    /// Interface for well state comparision
    /// </summary>
    public interface IWellStateEvaluator
    {
        /// <summary>
        /// Method that evaluates well state
        /// </summary>
        /// <param name="wellState">Well state to be evaluated</param>
        /// <returns>Integer value which represents rank of the well state (higher is better)</returns>
        int Evaluate(WellState wellState);
    }
}
