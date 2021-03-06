﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongGame.BL
{
    public class TextButton : Component
    {
        #region fields
        private MouseState currentMouse;
        private SpriteFont font;
        private bool isHovering;
        private MouseState previouseMouse;
        private Texture2D texture;
        #endregion

        #region Properties
        public event EventHandler Click;
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        public string Text { get; set; }
        #endregion

        #region Methods
        public TextButton(Texture2D texture, SpriteFont spriteFont)
        {
            this.texture = texture;
            this.font = spriteFont;
            PenColor = Color.Black;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var _color = Color.White;
            if (isHovering)
            {
                _color = Color.Gray;
            }

            spriteBatch.Draw(texture, Rectangle, _color);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font, Text, new Vector2(x, y), PenColor);
            }
        }
        public override void Update(GameTime gameTime)
        {
            this.previouseMouse = currentMouse;
            this.currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            this.isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;
                if (currentMouse.LeftButton == ButtonState.Released &&
                    previouseMouse.LeftButton == ButtonState.Pressed
                    )
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
        #endregion
    }
}
