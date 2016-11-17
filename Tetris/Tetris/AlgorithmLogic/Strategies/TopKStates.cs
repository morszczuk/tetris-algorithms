using System.Collections.Generic;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Strategies
{
    public class TopKStates : IWellStateSelectionStrategy
    {
        public int K { get; private set; }
        public IWellStateEvaluator Evaluator { get; private set; }

        public TopKStates(int k, IWellStateEvaluator evaluator)
        {
            Evaluator = evaluator;
            K = k;
        }

        public List<WellState> Select(List<WellState> wellStates)
        {
            wellStates.Sort(Comparator);
            return wellStates.GetRange(0, K);
        }

        private int Comparator(WellState s1, WellState s2)
        {
            return Evaluator.Evaluate(s2) - Evaluator.Evaluate(s1);
        }
    }
}
