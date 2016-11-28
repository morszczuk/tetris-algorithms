using System;
using System.Linq;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    /// <summary>
    /// Basic evaluator comparing by % of space covered without considering top N rows
    /// </summary>
    public class FillWithoutTopNEvaluator : IWellStateEvaluator
    {
        public int N { get; private set;  }

        public FillWithoutTopNEvaluator(int n)
        {
            N = n;
        }

        public int Evaluate(WellState wellState)
        {
            if (wellState.Fill.Count == 0) return 0;
            var n = N < wellState.Fill.Count ? N : N - wellState.Fill.Count;
            var tiles = wellState.TilesCount;
            for (var y = 0; y < n; y++)
                for (var x = 0; x < wellState.Well.Width; x++)
                    if(wellState.IsFilled(x, y)) tiles--;
            var ratio = (float)tiles / (float)((wellState.Fill.Count - n) * wellState.Well.Width);
            return Convert.ToInt32(ratio * 10000);
        }
    }
}
