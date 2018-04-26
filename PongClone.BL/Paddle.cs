using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame.BL
{
    public class Paddle : Sprite
    {
        public PlayerTypes PlayerType { get; }

        public enum PlayerTypes
        {
            Human,
            Computer
        }
        public Paddle(Texture2D texture, Vector2 location, Rectangle screenBound, PlayerTypes playerType) : base(texture, location, screenBound)
        {
            PlayerType = playerType;
        }
        public float YCenterOfBar => Location.Y + Height;
        public float XCenterOfBar => Location.X + Width;

        public override void Update(GameTime gameTime,GameObjects gameObjects)
        {
            if (PlayerType == PlayerTypes.Computer)
            {
                var random = new Random();

                var reactionThreshold = random.Next(10, 50);
                if(gameObjects.Ball.Location.Y+gameObjects.Ball.Height<Location.Y+reactionThreshold)
                    Velocity = new Vector2(0, -8f);
                if(gameObjects.Ball.Location.Y>Location.Y+Height+reactionThreshold)
                    Velocity = new Vector2(0, 8f);
                base.Update(gameTime, gameObjects);
            }
            if (PlayerType == PlayerTypes.Human)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    Velocity = new Vector2(0, -7.5f);
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    Velocity = new Vector2(0, 7.5f);
                base.Update(gameTime,gameObjects);
            }

        }
        public override void CheckBounds()
        {
            Location = new Vector2(Location.X,
                MathHelper.Clamp(Location.Y, 0, GameBoundaries.Height - Height));
        }

    }
}
