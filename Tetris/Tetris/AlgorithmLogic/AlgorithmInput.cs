using System;
using Tetris.Enums;
using Tetris.Models;

namespace Tetris.AlgorithmLogic
{
    /// <summary>
    /// Algorythm input parameters
    /// </summary>
    [Serializable]
    public class AlgorithmInput
    {
        /// <summary>
        /// Bricks collection with its' count
        /// </summary>
        public BricksShelf BricksShelf { get; set; }

        /// <summary>
        /// Number of wells = k parameter
        /// </summary>
        public int WellNo { get; set; }

        /// <summary>
        /// Well width
        /// </summary>
        public int WellWidth { get; set; }

        /// <summary>
        /// Type of running computations
        /// </summary>
        public AlgorithmsEnum AlgorithmType { get; set; }

        /// <summary>
        /// Evaluator used to rank well states
        /// </summary>
        public Type EvaluatorType { get; set; }

        /// <summary>
        /// True if algorithm is in a step mode
        /// </summary>
        public bool IsStep { get; set; }

        public AlgorithmInput(BricksShelf bricksShelf, int wellNo, int wellWidth, AlgorithmsEnum type,Type evalType,bool isStep)
        {
            BricksShelf = bricksShelf;
            WellWidth = wellWidth;
            WellNo = wellNo;
            AlgorithmType = type;
            EvaluatorType = evalType;
            IsStep = isStep;
        }

    }
}
