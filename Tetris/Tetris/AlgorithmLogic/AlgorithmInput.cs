using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public AlgorithmInput(BricksShelf bricksShelf, int wellNo, int wellWidth, AlgorithmsEnum type)
        {
            BricksShelf = bricksShelf;
            WellWidth = wellWidth;
            WellNo = wellNo;
            AlgorithmType = type;
        }

    }
}
