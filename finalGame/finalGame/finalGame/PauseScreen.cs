using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace finalGame
{
    class PauseScreen : GameScene
    {
        private MenuComponent menu;
        public MenuComponent Menu { get => menu; set => menu = value; }
        private string[] menuItems = { "Resume Game", "Mute", "Quit" };

        public PauseScreen(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.SpriteBatch = spriteBatch;

            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            SpriteFont hilightFont = game.Content.Load<SpriteFont>("Fonts/hilightFont");

            menu = new MenuComponent(game, spriteBatch, regularFont, hilightFont, menuItems);
            Components.Add(menu);
        }
    }
}
