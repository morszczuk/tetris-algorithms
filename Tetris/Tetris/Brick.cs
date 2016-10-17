namespace Tetris
{
    public class Brick
    {
        public int Width => Body.GetLength(1);

        public int Height => Body.GetLength(0);
        public int[,] Body { get; set; }

        public int Cardinality { get; set; }

        public Brick()
        {
            Cardinality = 1;
        }
    }
}
