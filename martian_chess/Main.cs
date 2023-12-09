using martian_chess.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace martian_chess
{
    public class Main : Game
    {

        Board board;
        Vector2 draggingOffset;
        private bool isDragging = false;
        Piece dragPiece = null;
        int[] prevPos = {-1, -1};
        List<Vector2> possibleMoves;
        bool currentPlayer = true; // нижний true, верхний false
        SpriteFont font;
        int[] score = { 0, 0 };


        public Main()
        {
            Global.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void InitScreen()
        {
            Global.cellSize = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75f) / 8;
            Global.width = Global.cellSize * 6;
            Global.height = Global.cellSize * 8;

            Global.graphics.PreferredBackBufferWidth = Global.width;
            Global.graphics.PreferredBackBufferHeight = Global.height;
            Global.graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            board = new Board(new Piece[4][]);
            board.Init();
   
            InitScreen();
            base.Initialize();
        }

        private void SetBoardClassic()
        {
            int[,] queensPos = { { 0, 1 }, { 0, 0 }, { 1, 0 }, { 3, 6 }, { 2, 7 }, { 3, 7 } };
            int[,] dronesPos = { { 0, 2 }, { 1, 1 }, { 2, 0 }, { 3, 5 }, { 2, 6 }, { 1, 7 } };
            int[,] pawsPos = { { 2, 1 }, { 2, 2 }, { 1, 2 }, { 2, 5 }, { 1, 5 }, { 1, 6 } };
            for (int i = 0; i < 6; i++)
            {
                board.figures[queensPos[i,0]][queensPos[i,1]] = new Queen(
                    new Vector2(queensPos[i, 0] * Global.cellSize,
                    queensPos[i, 1] * Global.cellSize), new Vector2(Global.cellSize, Global.cellSize));
                board.figures[dronesPos[i, 0]][dronesPos[i, 1]] = new Drone(
                    new Vector2(dronesPos[i, 0] * Global.cellSize,
                    dronesPos[i, 1] * Global.cellSize), new Vector2(Global.cellSize, Global.cellSize));
                board.figures[pawsPos[i, 0]][pawsPos[i, 1]] = new Paw(
                    new Vector2(pawsPos[i, 0] * Global.cellSize,
                    pawsPos[i, 1] * Global.cellSize), new Vector2(Global.cellSize, Global.cellSize));
            }
            
        }
        private void SetBoardNeo()
        {
            
            int[,] dronesPos = { { 0, 1 }, { 1, 1 }, { 1, 0 }, { 2, 7 }, { 2, 6 }, { 3, 6 } };
            int[,] pawsPos = { { 2, 0 }, { 2, 1 }, { 2, 2 }, { 1, 2 }, { 0, 2 }, { 3, 5 }, { 2, 5 }, { 1, 5 }, { 1, 6 }, { 1, 7 } };
            int[,] queensPos = { { 0, 0 }, { 3, 7 } };
            for (int i = 0; i < 2; i++)
            {
                board.figures[queensPos[i, 0]][queensPos[i, 1]] = new Queen(
                    new Vector2(queensPos[i, 0] * Global.cellSize,
                    queensPos[i, 1] * Global.cellSize), new Vector2(Global.cellSize, Global.cellSize));
            }
            for (int i = 0; i < 6; i++)
            {
                board.figures[dronesPos[i, 0]][dronesPos[i, 1]] = new Drone(
                    new Vector2(dronesPos[i, 0] * Global.cellSize,
                    dronesPos[i, 1] * Global.cellSize), new Vector2(Global.cellSize, Global.cellSize));
            }
            for (int i = 0; i < 10; i++)
            {
                board.figures[pawsPos[i, 0]][pawsPos[i, 1]] = new Paw(
                    new Vector2(pawsPos[i, 0] * Global.cellSize,
                    pawsPos[i, 1] * Global.cellSize), new Vector2(Global.cellSize, Global.cellSize));
            }
        }

        private void SetStartPlayers()
        {
            for (int x = 0; x < board.figures.Length; x++)
            {
                for (int y = 0; y < board.figures[x].Length; y++)
                {
                    if (board.figures[x][y] != null)
                    {
                        if (y < 4)
                        {
                            board.figures[x][y].player = false;
                        }
                        else
                        {
                            board.figures[x][y].player = true;
                        }
                        
                    }
                }
            }
        }

        private void SetBoardTest()
        {
            board.figures[1][1] = new Queen(
                    new Vector2(1 * Global.cellSize,
                    1 * Global.cellSize), new Vector2(Global.cellSize, Global.cellSize));
            board.figures[3][6] = new Drone(
                    new Vector2(3 * Global.cellSize,
                    6 * Global.cellSize), new Vector2(Global.cellSize, Global.cellSize));
        }
        protected override void LoadContent()
        {
            Global.spriteBatch = new SpriteBatch(GraphicsDevice);
            Global.content = this.Content;
            font = Global.content.Load<SpriteFont>("galleryFont");
            SetBoardNeo();
            SetStartPlayers();
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState mouseState = Mouse.GetState(); 
            
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                
                if (!isDragging )
                {
                    prevPos = IsMouseOverFigure(mouseState.Position);
                    
                    if (prevPos != null)
                    {
                        dragPiece = board.figures[prevPos[0]][prevPos[1]];
                        isDragging = true;
                        draggingOffset = (dragPiece.globalPositon - new Vector2(mouseState.X, mouseState.Y)) * 1.0f;
                        dragPiece.dragged = true;
                        possibleMoves = dragPiece.GetValibleMoves(prevPos, board);
                    }
                }
            }
            else
            {
                if (dragPiece != null)
                {
                    dragPiece.dragged = false;
                    Vector2 newPos = new Vector2(
                        (float)Math.Clamp(Math.Floor((dragPiece.globalPositon.X + Global.cellSize / 2) / Global.cellSize), 0, 3),
                        (float)Math.Clamp(Math.Floor((dragPiece.globalPositon.Y + Global.cellSize / 2) / Global.cellSize), 0, 7)
                    );
                    
                    if (possibleMoves.Contains(newPos) )
                    {
                        //Успешный ход
                        dragPiece.globalPositon = newPos * Global.cellSize;
                        if (newPos != new Vector2(prevPos[0], prevPos[1]) )
                        {
                            board.figures[prevPos[0]][prevPos[1]] = null;

                            if (newPos.Y < 4)
                            {
                                dragPiece.player = false;
                            }
                            else
                            {
                                dragPiece.player = true;
                            }
                            if (board.figures[(int)newPos.X][(int)newPos.Y] != null)
                            {
                                if (board.figures[(int)newPos.X][(int)newPos.Y].player)
                                {
                                    score[0] += board.figures[(int)newPos.X][(int)newPos.Y].coast;
                                }
                                else
                                {
                                    score[1] += board.figures[(int)newPos.X][(int)newPos.Y].coast;

                                }

                            }
                            board.figures[(int)newPos.X][(int)newPos.Y] = dragPiece;
                            currentPlayer = !currentPlayer;
                        }



                    }
                    else
                    {
                        dragPiece.globalPositon = new Vector2(prevPos[0], prevPos[1]) * Global.cellSize;
                    }

                    

                    dragPiece = null;
                    
                }    
                isDragging = false;
            }
            if (isDragging)
            {
                dragPiece.globalPositon.X = mouseState.X + draggingOffset.X;
                dragPiece.globalPositon.Y = mouseState.Y + draggingOffset.Y;

            }


            base.Update(gameTime);
        }

        private int[] IsMouseOverFigure(Point mousePosition)
        {
            for (int x = 0; x < board.figures.Length; x++)
            {
                for (int y = 0; y < board.figures[x].Length; y++)
                {
                    if (board.figures[x][y] != null)
                    {
                        if(currentPlayer == board.figures[x][y].player)
                        {
                            Rectangle droneRectangle = new Rectangle((int)board.figures[x][y].globalPositon.X, (int)board.figures[x][y].globalPositon.Y, (int)board.figures[x][y].size.X, (int)board.figures[x][y].size.Y);
                            if (droneRectangle.Contains(mousePosition))
                                return new int[] { x, y };
                        }
                        
                    }
                }
            }
            return null; 
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(23, 20, 20));
            Global.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            

            if (isDragging)
            {
                board.Draw(possibleMoves);
            }
            else
            {
                board.Draw();
            }
            
            for (int x = 0; x < board.figures.Length; x++)
            {
                for (int y = 0; y < board.figures[x].Length; y++)
                {
                    if (board.figures[x][y] != null)
                    {
                        if(isDragging && board.figures[x][y] != dragPiece)
                        {
                            board.figures[x][y].Draw();
                        }
                        else if (!isDragging)
                        {
                            board.figures[x][y].Draw();
                        }
                        
                    }
                }
            }
            if (isDragging)
            {
                dragPiece.Draw();
            }
            Global.spriteBatch.DrawString(font, "Score:\n" + score[0], new Vector2(Global.cellSize*4.25f, Global.cellSize * 0.25f), Color.White);
            Global.spriteBatch.DrawString(font, "Score:\n" + score[1], new Vector2(Global.cellSize * 4.25f, Global.cellSize * 4.25f), Color.White);
            Global.spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}

//TODO
/*
 * 1. Составить возможные хода для дрона и квины 
 * 2. Добавить трекинг очков
 * 3. Добавить окончание игры
 * 4. Добавить отчёт о победителе
 * 5. Добавить кнопку рестарта
 * 6. Добаить череду хода
 * 
 * ---------------------Если будет не лень---------------------------
 * 
 * 1. Добить трекниг совершенного хода
 * 2. Добавить игру по локальной сети
 */