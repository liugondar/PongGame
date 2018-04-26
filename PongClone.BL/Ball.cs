using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame.BL
{
    public class Ball : Sprite
    {
        public Paddle AttachPaddle { get; set; }
        public Ball(Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries)
        {
        }

        public void AttachTo(Paddle paddle)
        {
            AttachPaddle = paddle;
        }

        public override void CheckBounds()
        {
            bool isOutOfHeightBoundaries = Location.Y <= 0 ||
                GameBoundaries.Height <= (Location.Y + Height);

            if (isOutOfHeightBoundaries)
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
        }
        /// <summary>
        /// Update location and velocity of ball
        /// - Move depends paddle if ball attach to paddle
        /// - Fire the ball if player press space button when it attach to the paddle
        /// - Move with velocity in the other case
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="gameObjects"></param>
        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            bool doesUserPressEnterToPlay =
                Keyboard.GetState().IsKeyDown(Keys.Space) && AttachPaddle != null;
            if (doesUserPressEnterToPlay)
            {
                Velocity = new Vector2(9f, AttachPaddle.Velocity.Y);
                AttachPaddle = null;
            }

            if (AttachPaddle != null)
            {
                float x = AttachPaddle.Location.X + AttachPaddle.Width+10f;
                float y = AttachPaddle.Location.Y;
                Location = new Vector2(x, y);
            }
            else
            {
                CheckBallCollision(gameObjects.PlayerPaddle);
                CheckBallCollision(gameObjects.ComputerPaddle);

            }
            base.Update(gameTime, gameObjects);

        }

        private void CheckBallCollision(Sprite paddle)
        {
            bool isIntersectsToPaddle = BoundingBox.Intersects(paddle.BoundingBox);
            if (isIntersectsToPaddle)
            {
                //Velocity = new Vector2(-Velocity.X, Velocity.Y);
                this.Bounce(paddle);
            }
        }

        public void Bounce(Sprite paddle)
        {
            // Calculate a new direction depending on where on the paddle the ball bounces
            float differenceToTargetCenter = paddle.BoundingBox.Center.Y - BoundingBox.Center.Y;
            Velocity = Calc2D.GetRightPointingAngledPoint((int)(90 + (differenceToTargetCenter * 1.3f))) * 10;

            // Set a new position to make sure we're outside the paddle
            if (paddle.BoundingBox.Center.X > BoundingBox.Center.X)
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
                Location = new Vector2(paddle.BoundingBox.Left - Texture.Width, Location.Y);
            }
            else
            {
                Location =new Vector2( paddle.BoundingBox.Right,Location.Y);
            }
        }

    }
}
