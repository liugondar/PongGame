using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PongGame.BL;

namespace PongGame 
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Paddle playerPaddle;
        private Paddle computerPaddle;
        private Ball ball;
        private Score score;
        private Background background;
        private GameObjects gameObjects;
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

            base.Initialize();
            this.IsMouseVisible = true;
            this.Window.Position = new Point(200, 50); // xPos and yPos (pixel)
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var gameBoundaries = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            //Player 1 paddle
            using (var stream = TitleContainer.OpenStream("Content/Player1Paddle.png"))
            {
                var paddle1Texture = Texture2D.FromStream(this.GraphicsDevice, stream);
                var playerPaddleLocation = new Vector2(0, gameBoundaries.Height / 2-paddle1Texture.Height);
                playerPaddle = new Paddle(paddle1Texture,playerPaddleLocation, gameBoundaries, Paddle.PlayerTypes.Human);
            }
            //Player 2 paddle
            using (var stream = TitleContainer.OpenStream("Content/Player2Paddle.png"))
            {
                var paddle2Texture = Texture2D.FromStream(this.GraphicsDevice, stream);
                var computerPaddleLocation = new Vector2(gameBoundaries.Width - paddle2Texture.Width, gameBoundaries.Height/2-paddle2Texture.Height);
                computerPaddle = new Paddle(paddle2Texture, computerPaddleLocation, gameBoundaries, Paddle.PlayerTypes.Computer);
            }
            //Ball
            using (var stream = TitleContainer.OpenStream("Content/Ball.png"))
            {
                ball = new BL.Ball(Texture2D.FromStream(this.GraphicsDevice, stream), Vector2.Zero,
                   gameBoundaries);
            }

            using (var stream = TitleContainer.OpenStream("Content/Background.jpg"))
            {
                background = new BL.Background(Texture2D.FromStream(this.GraphicsDevice, stream),
                   gameBoundaries);
            }

            ball.AttachTo(playerPaddle);
            //Score
            score = new Score(Content.Load<SpriteFont>("GameFont"),gameBoundaries);
            gameObjects = new GameObjects { Ball = ball, PlayerPaddle = playerPaddle, ComputerPaddle = computerPaddle };
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            spriteBatch.Dispose();
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


            playerPaddle.Update(gameTime, gameObjects);
            ball.Update(gameTime, gameObjects);
            computerPaddle.Update(gameTime, gameObjects);
            score.Update(gameTime, gameObjects);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            background.Draw(spriteBatch);
            computerPaddle.Draw(spriteBatch);
            playerPaddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            score.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
