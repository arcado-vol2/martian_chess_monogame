using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace martian_chess
{
    public class Queen : Piece
    {
        public Queen(Vector2 globalPosition, Vector2 size) : base("queen", globalPosition, size)
        {
            this.coast = 3;
        }

        public override List<Vector2> GetValibleMoves(int[] self, Board board)
        {   
            List<Vector2> ans = new List<Vector2>() { new Vector2(self[0], self[1]) };
            for (int x = -1; x > -5; x--)
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
            for (int x = 1; x < 5; x++)
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

            for (int y = -1; y > -9; y--)
            {
                if (self[1] + y >= 0)
                {

                    if (board.figures[self[0]][self[1] + y] != null)
                    {
                        if (self[1] + y < 4 && player)
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
            for (int y = 1; y < 9; y++)
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
            //диагонали
            for (int i = 1; i < 4; i++)
            {
                if (self[0] + i < 4 && self[1] + i < 8)
                {
                    if (board.figures[self[0]+i][self[1] + i] != null)
                    {
                        if (self[1] + i > 3 && !player)
                            ans.Add(new Vector2(self[0] + i, self[1] + i));
                        break;
                    }
                    ans.Add(new Vector2(self[0] + i, self[1] + i));
                }
                else
                {
                    break;
                }
            }
            for (int i = 1; i < 4; i++)
            {
                if (self[0] - i >= 0 && self[1] + i < 8)
                {
                    if (board.figures[self[0] - i][self[1] + i] != null)
                    {
                        if (self[1] + i > 3 && !player)
                            ans.Add(new Vector2(self[0] - i, self[1] + i));
                        break;
                    }
                    ans.Add(new Vector2(self[0] - i, self[1] + i));
                }
                else
                {
                    break;
                }
            }
            for (int i = 1; i < 4; i++)
            {
                if (self[0] + i < 4 && self[1] - i >= 0)
                {
                    if (board.figures[self[0] + i][self[1] - i] != null)
                    {
                        if (self[1] - i < 4 && player)
                            ans.Add(new Vector2(self[0] + i, self[1] - i));
                        break;
                    }
                    ans.Add(new Vector2(self[0] + i, self[1] - i));
                }
                else
                {
                    break;
                }
            }
            for (int i = 1; i < 4; i++)
            {
                if (self[0] -  i >= 0 && self[1] - i >= 0)
                {
                    if (board.figures[self[0] - i][self[1] - i] != null)
                    {
                        if (self[1] - i < 4 && player)
                            ans.Add(new Vector2(self[0] -  i, self[1] - i));
                        break;
                    }
                    ans.Add(new Vector2(self[0] -  i, self[1] - i));
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
