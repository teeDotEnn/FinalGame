/* File: OptionScene.cs
 * Purpose: The option scene for the game
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
    /// The option scene class
    /// </summary>
    public class OptionScene : GameScene
    {
        private MenuComponent menu;
        /// <summary>
        /// THe menu component
        /// </summary>
        public MenuComponent Menu { get => menu; set => menu = value; }
        private string[] menuItems = { "Mute Game", "Return to Main Menu" };

        /// <summary>
        /// Instatiate a new option scene
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        public OptionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;

            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            SpriteFont hilightFont = game.Content.Load<SpriteFont>("Fonts/hilightFont");

            menu = new MenuComponent(game, spriteBatch, regularFont, hilightFont, menuItems);
            Components.Add(menu);
        }
    }
}
