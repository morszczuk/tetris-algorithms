using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Tetris.Models
{
    /// <summary>
    /// Object that calculates and stores possible rotations for brick type
    /// </summary>
    [Serializable]
    public class BrickType
    {
        public IEnumerable<RotateEnum> AvailableRotations => _rotations.Keys;
        public int DefaultCount { get; set; }
        public Brick BaseBrick => Brick();

        private readonly Dictionary<RotateEnum, Brick> _rotations;

        public BrickType(bool[,] body)
        {
            DefaultCount = 1;
            var brick = new Brick(body, this);
            _rotations = new Dictionary<RotateEnum, Brick>(4);
            foreach (RotateEnum rotation in Enum.GetValues(typeof(RotateEnum)))
            {
                if (_rotations.All(e => e.Value != brick))
                    _rotations.Add(rotation, brick);
                brick = Rotate90Right(brick);
            }
        }

        /// <summary>
        /// Extract brick types for given bricks shelf
        /// </summary>
        /// <param name="shelf">BricksShelf with brick types</param>
        /// <returns>List of BrickTypes extracted from BricksShelf</returns>
        public static IEnumerable<BrickType> GetBrickTypes(BricksShelf shelf)
        {
            return shelf.Bricks.Select(s => s.Key).ToList();
        }

        /// <summary>
        /// Returns Brick for given BrickType rotation
        /// </summary>
        /// <param name="rotation">One of possible brick rotations</param>
        /// <returns>Brick representation after applying rotation</returns>
        public Brick Brick(RotateEnum rotation = RotateEnum.Right0)
        {
            return _rotations[rotation];
        }

        /// <summary>
        /// Rotates the given brick 90 degrees clockwise
        /// </summary>
        /// <param name="brick">Brick to be rotated</param>
        /// <returns>new Brick with rotated body</returns>
        public static Brick Rotate90Right(Brick brick)
        {
            var newBody = new bool[brick.Width, brick.Height];
            for (var i = 0; i < brick.Height; i++)
                for (var j = 0; j < brick.Width; j++)
                    newBody[j, brick.Height - i - 1] = brick.Body[i, j];
            return new Brick(newBody, brick.BrickType);
        }

        public static bool operator ==(BrickType b1, BrickType b2)
        {
            if ((object)b1 == null && (object)b2 == null) return true;
            if ((object)b1 == null || (object)b2 == null) return false;
            return b1._rotations.Any(e => e.Value == b2.Brick());
        }

        public static bool operator !=(BrickType b1, BrickType b2)
        {
            return !(b1 == b2);
        }
    }
}
