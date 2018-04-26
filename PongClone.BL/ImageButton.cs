using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongGame.BL
{
    public class ImageButton : Component
    {
        private MouseState currentMouse;
        private bool isHovering;
        Color color = new Color(255, 255, 255, 255);
        private MouseState previouseMouse;
        private Texture2D texture;
        public Vector2 size;
        public event EventHandler Click;
        public Vector2 Position { get; set; }
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);

        public ImageButton(Texture2D texture, GraphicsDevice graphics)
        {
            this.texture = texture;
        }
        bool down;
        public bool isClicked;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, color);
        }
        public override void Update(GameTime gameTime)
        {
            this.previouseMouse = currentMouse;
            this.currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            this.isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                if (color.A == 255) down = false;
                if (color.A == 0) down = true;

                if (down) color.A += 6; else color.A -= 6;
                isHovering = true;
                if (currentMouse.LeftButton == ButtonState.Released &&
                    previouseMouse.LeftButton == ButtonState.Pressed
                    )
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }else if (color.A < 255)
            {
                color.A += 6;
                isClicked = false;
            }
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }
    }
}
