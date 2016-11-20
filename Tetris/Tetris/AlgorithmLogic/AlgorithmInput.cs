using System;
using Tetris.Enums;
using Tetris.Models;

namespace Tetris.AlgorithmLogic
{
    public class AlgorithmInput
    {
        public BricksShelf BricksShelf { get; set; }

        public int WellNo { get; set; }

        public int WellWidth { get; set; }

        public AlgorithmsEnum AlgorithmType { get; set; }
        public Type EvaluatorType { get; set; }

        public AlgorithmInput(BricksShelf bricksShelf, int wellNo, int wellWidth, AlgorithmsEnum type,Type evalType)
        {
            BricksShelf = bricksShelf;
            WellWidth = wellWidth;
            WellNo = wellNo;
            AlgorithmType = type;
            EvaluatorType = evalType;
        }

    }
}
