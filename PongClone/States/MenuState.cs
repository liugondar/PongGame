using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PongGame.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PongGame.States
{
    public class MenuState : State
    {
        private List<BL.Component> components;
        private Background background;
        public Rectangle GameBoundaries=> new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
        private MouseState currentMouse;
        private MouseState previouseMouse;

        public event EventHandler Click;


        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            BL.Button newGameButton;
            using (var stream = TitleContainer.OpenStream("Content/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>("Font");
                var buttonTexture = Texture2D.FromStream(this.graphicsDevice, stream);
                newGameButton = new BL.Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(600, 200),
                    Text = "New Game"
                };
                newGameButton.Click += NewgameButton_Click;
            }

            BL.Button quitGameButton;
            using (var stream = TitleContainer.OpenStream("Content/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>("Font");
                var buttonTexture = Texture2D.FromStream(this.graphicsDevice, stream);
                quitGameButton = new BL.Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(600, 250),
                    Text = "Quit Game"
                };
                quitGameButton.Click += QuitgameButton_Click;
            }

            using (var stream = TitleContainer.OpenStream("Content/MenugameBackground.jpg"))
            {
                var backgroundTexture = Texture2D.FromStream(this.graphicsDevice, stream);
                background = new Background(backgroundTexture, GameBoundaries);
            }

            // load component
            components = new List<Component>()
            {
                newGameButton,quitGameButton
            };
        }

        private void QuitgameButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        private void NewgameButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, graphicsDevice, content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(gameTime, spriteBatch);
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
            background.Update(gameTime);
        }
     
    }
}
