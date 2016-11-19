using System;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    public class FillEvaluator : IWellStateEvaluator
    {
        public FillEvaluator() { }

        public int Evaluate(WellState wellState)
        {
            var ratio = (float)wellState.TilesCount / (float)(wellState.Fill.Count*wellState.Well.Width);
            return Convert.ToInt32(ratio * 10000);
        }

    }
}