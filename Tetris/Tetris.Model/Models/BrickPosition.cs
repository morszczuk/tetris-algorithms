namespace Tetris.Model.Models
{
    public class BrickPosition
    {
        public Brick Brick { get; private set; }
        public RotateEnum Rotation { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public BrickPosition(Brick brick, int x, int y, RotateEnum rotation)
        {
            Brick = brick;
            X = x;
            Y = y;
            Rotation = rotation;
        }

    }
}