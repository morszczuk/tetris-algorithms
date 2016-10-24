using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Helpers
{
    class BricksSaver
    {
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
                        if (br.Body[i, j] != 0)
                        {
                            sb.Append("1");
                        }
                        else
                        {
                            sb.Append("0");
                        }
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
