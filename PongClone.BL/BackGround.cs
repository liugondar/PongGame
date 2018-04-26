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
    public class Background
    {
        Texture2D texture;
        Rectangle gameBoundaries;
        public Background()
        {
        }

        public Background(Texture2D texture, Rectangle gameBoundaries)
        {
            this.texture = texture;
            this.gameBoundaries = gameBoundaries;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, gameBoundaries, Color.Azure);
        }
    }
}
