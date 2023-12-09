using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace martian_chess
{
    public class Paw : Piece
    {
        public Paw( Vector2 globalPosition, Vector2 size) : base("paw", globalPosition, size)
        {
            this.coast = 1;
        }

        public override List<Vector2> GetValibleMoves(int[] self, Board board)
        {
            List<Vector2> ans = new List<Vector2>()
            {
                new Vector2(self[0], self[1]),
            };
            if (self[0] - 1 >= 0 && self[1] - 1 >= 0 &&
                board.figures[self[0] - 1][self[1] - 1] == null || (this.player && self[1] - 1 < 4))
                ans.Add(new Vector2(self[0] - 1, self[1] - 1));
            if (self[0] - 1 >= 0 && self[1] + 1 < 8 &&
                board.figures[self[0] - 1][self[1] + 1] == null || (!this.player && self[1] + 1 >= 4))
                ans.Add(new Vector2(self[0] - 1, self[1] + 1));
            if (self[0] + 1 < 4 && self[1] - 1 >= 0 &&
                board.figures[self[0] + 1][self[1] - 1] == null || (this.player && self[1] - 1 < 4))
                ans.Add(new Vector2(self[0] + 1, self[1] - 1));
            if (self[0] + 1 < 4 && self[1] + 1 < 8 &&
                board.figures[self[0] + 1][self[1] + 1] == null || (!this.player && self[1] + 1 >= 4))
                ans.Add(new Vector2(self[0] + 1, self[1] + 1));
            return ans;
        }

    }
}
