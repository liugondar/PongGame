using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame.BL
{
    public class Score
    {
        public Score(SpriteFont font, Rectangle gameBoundaries)
        {
            Font = font;
            GameBoundaries = gameBoundaries;
        }
        public int  PlayerScore { get; set; }
        public int ComputerScore { get; set; }
        public SpriteFont Font { get; }
        public Rectangle GameBoundaries { get; }
        public void Draw(SpriteBatch spriteBatch) {
            var scoreText = string.Format("{0} : {1}", PlayerScore, ComputerScore);
            var xPosition = (GameBoundaries.Width / 2) - (Font.MeasureString(scoreText).X/2);
            var position = new Vector2(xPosition, GameBoundaries.Height - 100);

            spriteBatch.DrawString(Font,scoreText,position,Color.AliceBlue);
        }

        public void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (gameObjects.Ball.Location.X > GameBoundaries.Width)
            {
                PlayerScore++;
                gameObjects.Ball.AttachTo(gameObjects.PlayerPaddle);
            }
            if (gameObjects.Ball.Location.X +gameObjects.Ball.Width <= 0)
            {
                ComputerScore++;
                gameObjects.Ball.AttachTo(gameObjects.PlayerPaddle);
            }
        }
    }
}