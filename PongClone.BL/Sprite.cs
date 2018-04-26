using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame.BL
{
    public abstract class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Direction { get; set; }
        public Rectangle GameBoundaries { get; }
        public Vector2 Velocity { get; set; }
        public int Width => Texture.Width;
        public int Height => Texture.Height;
        public Rectangle BoundingBox => new Rectangle((int)Location.X, (int)Location.Y, Width, Height);
        
        public Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries)
        {
            this.Texture = texture;
            this.Location = location;
            GameBoundaries = gameBoundaries;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, Color.White);
        }
        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            Location += Velocity;
            CheckBounds();
        }
        public abstract void CheckBounds();

    }
}