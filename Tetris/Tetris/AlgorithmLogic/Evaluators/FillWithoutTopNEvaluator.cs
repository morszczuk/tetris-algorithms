using System;
using System.Linq;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    public class FillWithoutTopNEvaluator : IWellStateEvaluator
    {
        public int N { get; private set;  }

        public FillWithoutTopNEvaluator(int n)
        {
            N = n;
        }

        public int Evaluate(WellState wellState)
        {
            var n = N < wellState.Fill.Count ? N : N - wellState.Fill.Count;
            var tiles = wellState.TilesCount;
            for (var i = 0; i < n; i++)
                tiles -= wellState.Fill[wellState.Fill.Count-i-1].Count(t => t);
            var ratio = (float)tiles / (float)((wellState.Fill.Count - n) * wellState.Well.Width);
            return Convert.ToInt32(ratio * 10000);
        }
    }
}
