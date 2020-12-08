using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllInOne
{
    public class StartScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private MenuComponent menu;
        private string[] menuItems = { "Start Game", "Help", "High Score", "Credits", "Quit" };

        public MenuComponent Menu { get => menu; set => menu = value; }

        public StartScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            SpriteFont highlightFont = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            menu = new MenuComponent(game, spriteBatch, regularFont, highlightFont, menuItems);
            this.Components.Add(menu);
        }

        
    }
}
