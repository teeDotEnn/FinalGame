/* File: PauseScreen.cs
 * Purpose: The pause scereen for the game
 * Rev History:
 *          Created 2020-12-09
 *          Stephen Draper

 */


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
    /// <summary>
    /// The pause scene
    /// </summary>
    public class PauseScreen : GameScene
    {
        private MenuComponent menu;
        /// <summary>
        /// Menu componeents
        /// </summary>
        public MenuComponent Menu { get => menu; set => menu = value; }
        private string[] menuItems = { "Resume Game", "Mute", "Quit" };
        /// <summary>
        /// instatiate a new game menu. Note that in this iteration, the menu items are fixed.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
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
