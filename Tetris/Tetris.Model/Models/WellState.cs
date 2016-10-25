using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Model.Models
{
    public class WellState
    {
        private Well _well;
        private List<BrickPosition> _bricksPositions;
        
        public WellState(Well well, List<BrickPosition> bricksPositions)
        {
            _well = well;
            _bricksPositions = bricksPositions;
        }
    }
}
