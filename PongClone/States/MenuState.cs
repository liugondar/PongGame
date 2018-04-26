using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PongGame.BL;
using PongGame.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongGame.States
{
    public class MenuState : State
    {
        private List<Component> components;
        private Controls.Background background;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            Button newGameButton;
            using (var stream = TitleContainer.OpenStream("Content/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>("Font");
                var buttonTexture = Texture2D.FromStream(this.graphicsDevice, stream);
                newGameButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(300, 200),
                    Text = "New Game"
                };
                newGameButton.Click += NewgameButton_Click;
            }

            Button loadGameButton;
            using (var stream = TitleContainer.OpenStream("Content/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>("Font");
                var buttonTexture = Texture2D.FromStream(this.graphicsDevice, stream);
                loadGameButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(300, 250),
                    Text = "Load Game"
                };
                loadGameButton.Click += LoadgameButton_Click;
            }

            Button quitGameButton;
            using (var stream = TitleContainer.OpenStream("Content/Button.png"))
            {
                var buttonFont = content.Load<SpriteFont>("Font");
                var buttonTexture = Texture2D.FromStream(this.graphicsDevice, stream);
                quitGameButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(300, 300),
                    Text = "Quit Game"
                };
                quitGameButton.Click += QuitgameButton_Click;
            }

            using (var stream = TitleContainer.OpenStream("Content/StartgameBackground.jpg"))
            {
                var backgroundTexture = Texture2D.FromStream(this.graphicsDevice, stream);
                var rectangle = new Rectangle(game.Window.Position.X, game.Window.Position.Y, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
                background = new Controls.Background(backgroundTexture, rectangle);
            }

            // load component
            components = new List<Component>()
            {
                newGameButton,loadGameButton,quitGameButton
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

        private void LoadgameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Game");
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
        }

    }
}
