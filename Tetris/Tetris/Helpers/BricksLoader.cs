using System;
using System.Collections.Generic;
using System.IO;

namespace Tetris.Helpers
{
    class BricksLoader
    {
        private char _separator = ' ';

        private readonly StreamReader _stream;

        private int _wellWidth;

        public BricksLoader(StreamReader stream)
        {
            _stream = stream;
        }

        public FileInputResult ReadFile()
        {          
            ProcessLineZero();
            Brick brick;
            List<Brick> bricks=new List<Brick>();

            while ((brick = ProcessBrick()) != null)
            {
                bricks.Add(brick);
            }

            return new FileInputResult(_wellWidth,bricks);
        }

        private string GetLine()
        {
            return _stream.ReadLine();
        }

        private void ProcessLineZero()
        {
            var values = GetLine().Split(_separator);
            _wellWidth = Convert.ToInt32(values[0]);

        }

        private Brick ProcessBrick()
        {
            var line = GetLine();
            if (line == null) return null;
            var values = line.Split(_separator);
            int width = Convert.ToInt32(values[0]);
            int height = Convert.ToInt32(values[1]);

            int [,] brickBody=new int[height,width];

            for (int i = 0; i < height; i++)
            {
                var row = GetLine().Split(_separator);
                for (int j = 0; j < row.Length; j++)
                {
                    brickBody[i, j] = Convert.ToInt32(row[j]);
                }
            }

            return new Brick()
            {
                Body = brickBody
            };      
        }
        

    }
}
