using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace martian_chess
{
    public class Piece
    {
        public int coast;
        public Vector2 globalPositon, size;
        public Texture2D texture;
        public bool player;
        public bool dragged;
        public Piece(string path, Vector2 globalPosition, Vector2 size)
        { 
            this.globalPositon = globalPosition;
            this.texture = Global.content.Load<Texture2D>(path);
            this.size = size;
        }
        public virtual void Update()
        {

        }

        public Vector2 GetBoardPos()
        {
            return new Vector2((int)this.globalPositon.X / Global.cellSize, (int)this.globalPositon.Y / Global.cellSize);
        }
        
        public virtual List<Vector2> GetValibleMoves(int[] self, Board board)
        {
            return null;
        }
        public virtual void Draw()
        {
            Vector2 newSize = this.dragged ? this.size * 1.1f : this.size ;
            Global.spriteBatch.Draw(this.texture, new Rectangle((int)globalPositon.X, (int)globalPositon.Y, (int)(newSize.X), (int)(newSize.Y)),
                            null, player ? Color.LightPink : Color.LightSkyBlue, 0.0f,
                            new Vector2(0, 0), new SpriteEffects(), 0);
            
        }
    }
}
