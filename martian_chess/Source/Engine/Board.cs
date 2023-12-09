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
using System.ComponentModel.Design;

namespace martian_chess
{
    public class Board
    {
        private static Color _darkOne = new Color(99, 86, 86);
        private static Color _darkTwo = new Color(56, 38, 38);
        private static Color _lightOne = new Color(243, 176, 90);
        private static Color _lightTwo = new Color(244, 106, 78);
        private static Color _green = new Color(44, 163, 76);

        private Texture2D rectangleTexture;

        public Piece[][] figures;

        public Board(Piece[][] figures)
        {
            this.figures = figures;
            for (int y = 0; y < 4; y++)
            {
                this.figures[y] = new Piece[8];
            }
        }
        public void Init() 
        {
            rectangleTexture = new Texture2D(Global.graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            rectangleTexture.SetData(new[] { Color.White });
        }
        public void Draw(List<Vector2> possibleMoves = null)
        {
            for (int x = 0; x < 4; x++) 
            {
                for (int y = 0; y < 8; y++)
                {
                    Color dark = y < 4 ? _darkOne: _darkTwo;
                    Color light = y < 4 ? _lightOne: _lightTwo;
                    if (possibleMoves != null)
                    {
                        if (possibleMoves.Contains(new Vector2(x, y)))
                            DrawCell(new Vector2(x * Global.cellSize, y * Global.cellSize), _green);
                        else
                            DrawCell(new Vector2(x * Global.cellSize, y * Global.cellSize), (x + y) % 2 == 0 ? dark : light);
                    }
                    else
                    {
                        DrawCell(new Vector2(x * Global.cellSize, y * Global.cellSize), (x + y) % 2 == 0 ? dark : light);
                    }
                    
                }
            }
        }

        private void DrawCell(Vector2 position, Color color)
        {
            Global.spriteBatch.Draw(rectangleTexture,
                new Rectangle((int)position.X, (int)position.Y, Global.cellSize, Global.cellSize), null,
                new Color(0,0,0), 0, new Vector2(0, 0), SpriteEffects.None, 0);

            Global.spriteBatch.Draw(rectangleTexture,
                new Rectangle((int)position.X, (int)position.Y, Global.cellSize-2, Global.cellSize-2), null,
                color, 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
