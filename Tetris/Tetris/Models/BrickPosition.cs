namespace Tetris.Models
{
    public class BrickPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public BrickType Brick { get; }
        public RotateEnum Rotation { get; }
        public BrickBody Body => Brick.Body(Rotation);

        public BrickPosition(BrickType brick, int x, int y, RotateEnum rotation)
        {
            Brick = brick;
            X = x;
            Y = y;
            Rotation = rotation;
        }
    }
}
