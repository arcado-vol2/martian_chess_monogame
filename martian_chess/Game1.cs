using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace martian_chess
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D droneTexture;
        private Vector2 dronePosition;
        private Vector2 offset;

        private bool isDragging = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the drone texture
            droneTexture = Content.Load<Texture2D>("drone");

            // Initialize drone position
            dronePosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - droneTexture.Width / 2,
                                        GraphicsDevice.Viewport.Height / 2 - droneTexture.Height / 2);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Check for mouse input
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (IsMouseOverDrone(mouseState.Position) && !isDragging)
                {
                    // Start dragging
                    isDragging = true;
                    offset = dronePosition - new Vector2(mouseState.X, mouseState.Y);
                }
            }
            else
            {
                // Stop dragging
                isDragging = false;
            }

            if (isDragging)
            {
                // Update drone position while dragging
                dronePosition.X = mouseState.X + offset.X;
                dronePosition.Y = mouseState.Y + offset.Y;
            }

            base.Update(gameTime);
        }

        private bool IsMouseOverDrone(Point mousePosition)
        {
            Rectangle droneRectangle = new Rectangle((int)dronePosition.X, (int)dronePosition.Y, droneTexture.Width, droneTexture.Height);
            return droneRectangle.Contains(mousePosition);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Draw the drone
            spriteBatch.Draw(droneTexture, dronePosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
