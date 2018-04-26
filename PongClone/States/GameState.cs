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
        private BL.Button quitGameButton;
        private BL.Button backmenuButton;
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
            using (var stream = TitleContainer.OpenStream("Content/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>("Font");
                var buttonTexture = Texture2D.FromStream(this.graphicsDevice, stream);
                var xPosition = 10;
                var yPositon = gameBoundaries.Height - 10 - buttonTexture.Height;
                backmenuButton = new BL.Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPositon),
                    Text = "Back Menu "
                };
                backmenuButton.Click += backmenuButton_Click;
            }

            using (var stream = TitleContainer.OpenStream("Content/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>("Font");
                var buttonTexture = Texture2D.FromStream(this.graphicsDevice, stream);
                var xPosition = gameBoundaries.Width-10-buttonTexture.Width  ;
                var yPositon = gameBoundaries.Height-10-buttonTexture.Height;
                quitGameButton = new BL.Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(xPosition, yPositon),
                    Text = "Quit Game"
                };
                quitGameButton.Click += QuitgameButton_Click;
            }
            ball.AttachTo(playerPaddle);
            //Score
            score = new Score(content.Load<SpriteFont>("GameFont"), gameBoundaries);
            gameObjects = new GameObjects { Ball = ball, PlayerPaddle = playerPaddle, ComputerPaddle = computerPaddle };

        }
        private void QuitgameButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        private void backmenuButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new MenuState(game, graphicsDevice, content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(gameTime, spriteBatch);
            backmenuButton.Draw(gameTime, spriteBatch);
            quitGameButton.Draw(gameTime, spriteBatch);
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
            backmenuButton.Update(gameTime);
            quitGameButton.Update(gameTime);
        }
    }
}
