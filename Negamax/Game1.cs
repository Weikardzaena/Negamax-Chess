using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Negamax
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const string PIECES_PATH = @".\Assets\Pieces\";
        const string BOARD_PATH = @".\Assets\Board\";

        Texture2D T_BishopWhite;
        Texture2D T_BishopBlack;
        Texture2D T_KingWhite;
        Texture2D T_KingBlack;
        Texture2D T_KnightWhite;
        Texture2D T_KnightBlack;
        Texture2D T_PawnWhite;
        Texture2D T_PawnBlack;
        Texture2D T_QueenWhite;
        Texture2D T_QueenBlack;
        Texture2D T_RookWhite;
        Texture2D T_RookBlack;

        Texture2D T_SquareDark;
        Texture2D T_SquareLight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            T_BishopWhite = Content.Load<Texture2D>(PIECES_PATH + "Bishop_White");
            T_BishopBlack = Content.Load<Texture2D>(PIECES_PATH + "Bishop_Black");
            T_KingWhite = Content.Load<Texture2D>(PIECES_PATH + "King_White");
            T_KingBlack = Content.Load<Texture2D>(PIECES_PATH + "King_Black");
            T_KnightWhite = Content.Load<Texture2D>(PIECES_PATH + "Knight_White");
            T_KnightBlack = Content.Load<Texture2D>(PIECES_PATH + "Knight_Black");
            T_PawnWhite = Content.Load<Texture2D>(PIECES_PATH + "Bishop_Black");
            T_PawnBlack = Content.Load<Texture2D>(PIECES_PATH + "Bishop_Black");
            T_QueenWhite = Content.Load<Texture2D>(PIECES_PATH + "Queen_White");
            T_QueenBlack = Content.Load<Texture2D>(PIECES_PATH + "Queen_Black");
            T_RookWhite = Content.Load<Texture2D>(PIECES_PATH + "Rook_White");
            T_RookBlack = Content.Load<Texture2D>(PIECES_PATH + "Rook_Black");

            T_SquareDark = Content.Load<Texture2D>(BOARD_PATH + "Square_Dark");
            T_SquareLight = Content.Load<Texture2D>(BOARD_PATH + "Square_Light");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            T_BishopWhite = null;
            T_BishopBlack = null;
            T_KingWhite = null;
            T_KingBlack = null;
            T_KnightWhite = null;
            T_KnightBlack = null;
            T_PawnWhite = null;
            T_PawnBlack = null;
            T_QueenWhite = null;
            T_QueenBlack = null;
            T_RookWhite = null;
            T_RookBlack = null;

            T_SquareDark = null;
            T_SquareLight = null;

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
                Exit();

            // TODO: Add your update logic here

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
            spriteBatch.Draw(T_SquareDark, new Rectangle(new Point(0), new Point(60)), Color.White);
            spriteBatch.Draw(T_SquareLight, new Rectangle(new Point(60), new Point(60)), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
