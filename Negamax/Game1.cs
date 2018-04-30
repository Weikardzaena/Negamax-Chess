using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Negamax.Board;

namespace Negamax
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState mPrevMouseState;

        StandardBoard mBoard;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = (StandardBoard.BOARD_DIM + 1) * Board.StandardBoard.SQUARE_DIM,
                PreferredBackBufferHeight = (StandardBoard.BOARD_DIM + 1) * Board.StandardBoard.SQUARE_DIM
            };
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // ONLY call this during LoadContent, not during Initialize()!
            // `Content` must be ready to go before initializing Board.
            mBoard = new StandardBoard(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                mBoard.ClearCurrentSelection();

            // TODO: Add your update logic here
            var newMouseState = Mouse.GetState();
            if ((newMouseState.LeftButton == ButtonState.Pressed) &&
                (mPrevMouseState.LeftButton == ButtonState.Released)) {
                mBoard.HandleClick(newMouseState.Position);
            }
            if ((newMouseState.RightButton == ButtonState.Pressed) &&
                (mPrevMouseState.RightButton == ButtonState.Released)) {
                mBoard.UndoLastMove();
            }

            mPrevMouseState = newMouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
                mBoard.DrawBoard(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
