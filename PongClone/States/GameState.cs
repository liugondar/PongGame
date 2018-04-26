using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PongGame.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongGame.States
{
    public class GameState : State
    {
        #region fields
        private Paddle playerPaddle;
        private Paddle computerPaddle;
        private Ball ball;
        private Score score;
        private Background background;
        private GameObjects gameObjects;
        #endregion

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var gameBoundaries = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            //Player 1 paddle
            using (var stream = TitleContainer.OpenStream("Content/Player1Paddle.png"))
            {
                var paddle1Texture = Texture2D.FromStream(this.graphicsDevice, stream);
                var playerPaddleLocation = new Vector2(0, gameBoundaries.Height / 2 - paddle1Texture.Height);
                playerPaddle = new Paddle(paddle1Texture, playerPaddleLocation, gameBoundaries, Paddle.PlayerTypes.Human);
            }
            //Player 2 paddle
            using (var stream = TitleContainer.OpenStream("Content/Player2Paddle.png"))
            {
                var paddle2Texture = Texture2D.FromStream(this.graphicsDevice, stream);
                var computerPaddleLocation = new Vector2(gameBoundaries.Width - paddle2Texture.Width, gameBoundaries.Height / 2 - paddle2Texture.Height);
                computerPaddle = new Paddle(paddle2Texture, computerPaddleLocation, gameBoundaries, Paddle.PlayerTypes.Computer);
            }
            //Ball
            using (var stream = TitleContainer.OpenStream("Content/Ball.png"))
            {
                ball = new BL.Ball(Texture2D.FromStream(this.graphicsDevice, stream), Vector2.Zero,
                   gameBoundaries);
            }

            using (var stream = TitleContainer.OpenStream("Content/Background.jpg"))
            {
                background = new BL.Background(Texture2D.FromStream(this.graphicsDevice, stream),
                   gameBoundaries);
            }

            ball.AttachTo(playerPaddle);
            //Score
            score = new Score(content.Load<SpriteFont>("GameFont"), gameBoundaries);
            gameObjects = new GameObjects { Ball = ball, PlayerPaddle = playerPaddle, ComputerPaddle = computerPaddle };

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            computerPaddle.Draw(spriteBatch);
            playerPaddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            score.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            playerPaddle.Update(gameTime, gameObjects);
            ball.Update(gameTime, gameObjects);
            computerPaddle.Update(gameTime, gameObjects);
            score.Update(gameTime, gameObjects);
        }
    }
}
