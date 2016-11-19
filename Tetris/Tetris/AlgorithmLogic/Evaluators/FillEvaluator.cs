using System;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    public class FillEvaluator : IWellStateEvaluator
    {
        public static readonly int MaxValue = 10000;

        public int Evaluate(WellState wellState)
        {
            if (wellState.Fill.Count == 0) return 0;
            var ratio = (float)wellState.TilesCount / (float)(wellState.Fill.Count*wellState.Well.Width);
            return Convert.ToInt32(ratio * MaxValue);
        }

    }
}