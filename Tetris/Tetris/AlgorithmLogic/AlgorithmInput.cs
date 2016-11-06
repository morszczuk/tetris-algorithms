using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Enums;

namespace Tetris.AlgorithmLogic
{
    public class AlgorithmInput
    {
        public List<Brick> Bricks { get; set; }

        public int WellNo { get; set; }

        public int WellWidth { get; set; }

        public AlgorithmsEnum AlgorithmType { get; set; }

        public AlgorithmInput(IEnumerable<Brick> bricks,int wellNo,int wellWidth,AlgorithmsEnum type)
        {
            Bricks = new List<Brick>(bricks);
            WellWidth = wellWidth;
            WellNo = wellNo;
            AlgorithmType = type;
        }

    }
}
