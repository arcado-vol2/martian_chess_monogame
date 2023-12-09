using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace martian_chess.Source.Engine
{
    public class Drone : Piece
    {
        public Drone(Vector2 globalPosition, Vector2 size) : base("drone", globalPosition, size)
        {
            this.coast = 2;
        }

        public override List<Vector2> GetValibleMoves(int[] self, Board board)
        {
            List<Vector2> ans = new List<Vector2>()
            {
                new Vector2(self[0], self[1])
            };

            for (int x = -1; x>-3; x--)
            {
                if (self[0] + x >= 0)
                {
                    if (board.figures[self[0] + x][self[1]] != null)
                        break;
                    ans.Add(new Vector2(self[0] + x, self[1]));
                }
                else
                {
                    break;
                }
            }
            for (int x = 1; x < 3; x++)
            {
                if (self[0] + x < 4)
                {
                    if (board.figures[self[0] + x][self[1]] != null)
                        break;
                    ans.Add(new Vector2(self[0] + x, self[1]));
                }
                else
                {
                    break;
                }
            }

            for (int y = -1; y > -3; y--)
            {
                if (self[1] + y >= 0)
                {

                    if (board.figures[self[0]][self[1] + y] != null)
                    {
                        if (self[1] + y < 4 && player)
                        ans.Add(new Vector2(self[0], self[1] + y));
                        break;
                    }
                    ans.Add(new Vector2(self[0], self[1]+y));
                }
                else
                {
                    break;
                }
            }
            for (int y = 1; y < 3; y++)
            {
                if (self[1] + y < 8)
                {

                    if (board.figures[self[0]][self[1] + y] != null)
                    {
                        if (self[1] + y > 3 && !player)
                            ans.Add(new Vector2(self[0], self[1] + y));
                        break;
                    }
                    ans.Add(new Vector2(self[0], self[1] + y));
                }
                else
                {
                    break;
                }
            }


            return ans;
        }
    }
}
