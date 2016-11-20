using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Models;

namespace Tetris.Helpers
{
    class BricksSaver
    {
        public static string SaveToFile(List<BrickType> bricks, int wellWidth)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine($"{wellWidth} {bricks.Count}");

            foreach (var br in bricks)
            {
                var brBody = br.Body();
                sb.AppendLine($"{brBody.Width} {brBody.Height}");
                for (int i = 0; i < brBody.Height; i++)
                {
                    for (int j = 0; j < brBody.Width; j++)
                    {
                        if (brBody.Body[i, j])
                        {
                            sb.Append("1");
                        }
                        else
                        {
                            sb.Append("0");
                        }
                        if (j != brBody.Width - 1)
                            sb.Append(" ");
                    }
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
    }
}
