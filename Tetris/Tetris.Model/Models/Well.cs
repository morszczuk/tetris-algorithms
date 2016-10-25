using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Model.Models
{
    public class Well
    {
        public int Width { get; private set; }

        public Well(int width)
        {
            Width = width;
        }
    }
}
