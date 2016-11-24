using System;
using System.Collections.Generic;
using System.IO;
using Tetris.Models;

namespace Tetris.Helpers
{
    /// <summary>
    /// Loader of bricks with given schema
    /// </summary>
    internal class BricksLoader
    {
        private char _separator = ' ';

        private readonly StreamReader _stream;

        private int _wellWidth;

        /// <summary>
        /// Loads bricks with given stream
        /// </summary>
        /// <param name="stream">stream with brick's data</param>
        public BricksLoader(StreamReader stream)
        {
            _stream = stream;
        }
        /// <summary>
        /// Returns all data from given stream
        /// </summary>
        /// <returns></returns>
        public FileInputResult ReadFile()
        {          
            ProcessLineZero();
            BrickType brick;
            var bricks=new List<BrickType>();

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

        private BrickType ProcessBrick()
        {
            var line = GetLine();
            if (line == null) return null;
            var values = line.Split(_separator);
            var width = Convert.ToInt32(values[0]);
            var height = Convert.ToInt32(values[1]);

            var brickBody = new bool[height,width];

            for (var i = 0; i < height; i++)
            {
                var row = GetLine().Split(new []{_separator},StringSplitOptions.RemoveEmptyEntries);
                for (var j = 0; j < row.Length; j++)
                {
                    brickBody[i, j] = row[j].Equals("1");
                }
            }

            return new BrickType(brickBody);
        }
    }
}
