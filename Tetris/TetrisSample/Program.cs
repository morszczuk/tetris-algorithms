using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.AlgorithmLogic;
using Tetris.Enums;
using Tetris.Models;

namespace TetrisSample
{
    class Program
    {
        public static void Main(string[] args)
        {
            var brickShelf = GenerateBricksShelf();
            var input = new AlgorithmInput(brickShelf, 2, 10, AlgorithmsEnum.Continuous);

            var executor = new AlgorithmExecutor(input);
            executor.Run();

            foreach (var wellState in executor.ActiveStates)
                PrintState(wellState);

            Console.ReadLine();
        }

        private static void Draw(ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.Write("  ");
            Console.ResetColor();
        }

        private static void PrintState(WellState wellState)
        {
            var width = wellState.Well.Width;

            Console.WriteLine("");
            Console.WriteLine("");
            for (var y = wellState.Fill.Count - 1; y >= 0; y--)
            {
                Draw(ConsoleColor.White);
                for (var x = 0; x < width; x++)
                    Draw(wellState.IsFilled(x,y) ? ConsoleColor.Yellow : ConsoleColor.Black);
                Draw(ConsoleColor.White);
                Console.WriteLine("");
            }
            for (var x = 0; x < width + 2; x++) Draw(ConsoleColor.White);
            Console.WriteLine("");
            Console.WriteLine("");
        }

        private static BricksShelf GenerateBricksShelf()
        {
            var b1 = new bool[3, 3]
            {
            { true, false, true },
            { true, false, true },
            { true, true , true }
            };

            var b2 = new bool[3, 3]
            {
            { true, false, false },
            { true, true, true },
            { true, false, false  }
            };

            var b3 = new bool[3, 3]
            {
            { true, false, false },
            { true, false, false },
            { true, true , true  }
            };

            var bricks = new List<BrickType>()
            {
                new BrickType(b1),
                new BrickType(b2),
                new BrickType(b3)
            };
            var shelf = new BricksShelf(bricks);

            shelf.Bricks[bricks[0]] = 5;
            shelf.Bricks[bricks[1]] = 4;
            shelf.Bricks[bricks[2]] = 3;

            return shelf;
        }
    }
}
