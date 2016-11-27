using System.Collections.Generic;
using System.Text;
using Tetris.Models;

namespace Tetris.Helpers
{
    public class BricksSaver
    {
        /// <summary>
        /// Generate file's content
        /// </summary>
        /// <param name="bricks">collection</param>
        /// <param name="wellWidth">Well's width</param>
        /// <returns>file's content as string</returns>
        public static string SaveToFile(List<Brick> bricks, int wellWidth)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{wellWidth} {bricks.Count}");

            foreach (var br in bricks)
            {
                sb.AppendLine($"{br.Width} {br.Height}");
                for (int i = 0; i < br.Height; i++)
                {
                    for (int j = 0; j < br.Width; j++)
                    {
                        sb.Append(br.Body[i, j] ? "1" : "0");
                        if (j != br.Width - 1)
                            sb.Append(" ");
                    }
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
    }
}
