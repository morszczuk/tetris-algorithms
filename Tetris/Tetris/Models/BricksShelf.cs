using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris.Models
{
    /// <summary>
    /// Contatiner for bricks with its cardinality as dictionary
    /// </summary>
    [Serializable]
    public class BricksShelf
    {
        public Dictionary<BrickType, int> Bricks { get; }

        /// <summary>
        /// Returns available bricks
        /// </summary>
        public IEnumerable<BrickType> AvailableBrickTypes => Bricks.Where(b => b.Value > 0).Select(b => b.Key);

        public BricksShelf(IEnumerable<BrickType> bricks)
        {
            Bricks = new Dictionary<BrickType, int>();
            foreach (var brick in bricks)
            {
                Bricks.Add(brick, brick.DefaultCount);
            }
        }

        public BricksShelf(BricksShelf bricksShelf)
        {
            Bricks = new Dictionary<BrickType, int>(bricksShelf.Bricks);
        }

    }
}
