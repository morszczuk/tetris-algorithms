using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris.Models
{
    public class BricksShelf
    {
        public Dictionary<Brick, int> Bricks { get; }
        public BricksShelf(IEnumerable<Brick> bricks)
        {
            Bricks = new Dictionary<Brick, int>();
            foreach (var brick in bricks)
            {
                Bricks.Add(brick, brick.Cardinality);
            }
        }

        public BricksShelf(BricksShelf bricksShelf)
        {
            Bricks = new Dictionary<Brick, int>(bricksShelf.Bricks);
        }

        public IEnumerable<Brick> AvailableBricks()
        {
            return Bricks.Where(b => b.Value > 0).Select(b => b.Key);
        }

    }
}
