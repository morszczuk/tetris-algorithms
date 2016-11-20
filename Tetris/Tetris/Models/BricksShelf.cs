using System.Collections.Generic;
using System.Linq;

namespace Tetris.Models
{
    public class BricksShelf
    {
        public Dictionary<BrickType, int> Bricks { get; }
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
