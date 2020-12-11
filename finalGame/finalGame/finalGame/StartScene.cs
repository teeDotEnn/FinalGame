/* File Name: StartScene.cs
 * Purpose: starting scene
 * Rev History:
 *      Created 2020-12-09
 *      Stephen Draper
 *      Documented 2020-12-11
 *      Timothy Nigh
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
    /// The start scene class
    /// </summary>
    public class StartScene : GameScene
    {
        private MenuComponent menu;
        /// <summary>
        /// Get the menu
        /// </summary>
        public MenuComponent Menu { get => menu; set => menu = value; }
        private string[] menuItems = { "Start Game", "Help", "Options", "High Scores", "Credits", "Quit" };
        
        /// <summary>
        /// Instatiates the start scene
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the spritebatch</param>
        public StartScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;

            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            SpriteFont hilightFont = game.Content.Load<SpriteFont>("Fonts/hilightFont");

            menu = new MenuComponent(game, spriteBatch, regularFont, hilightFont, menuItems);
            Components.Add(menu);
        }

        
    }
}
