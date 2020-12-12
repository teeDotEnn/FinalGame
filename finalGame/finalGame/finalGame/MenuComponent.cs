/* File: MenuComponent
 * Purpose: To render a menu
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
    /// A class used to display a menu
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont;
        private SpriteFont hilightFont;
        private List<string> menuItems;
        private int selectedIndex = 0;
        public int SelectedIndex { get => selectedIndex; set => selectedIndex = value; }

        private Vector2 position;
        private Color regularColor = Color.White;
        private Color hilightColor = Color.Yellow;

        private KeyboardState oldState;


        /// <summary>
        /// Instatiates a new menu
        /// </summary>
        /// <param name="game">the game object</param>
        /// <param name="spriteBatch">The spritebatch for drawing the menu</param>
        /// <param name="regularFont">The regular font for the menu items </param>
        /// <param name="hilightFont">The font to denote the selected menu item</param>
        /// <param name="menu">the items for the menu</param>
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regularFont, SpriteFont hilightFont, string[] menu) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            menuItems = menu.ToList<string>();
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
        }

        /// <summary>
        /// Draws the menu item, with the selected item bold
        /// </summary>
        /// <param name="gameTime"></param>

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();

            for (int i = 0; i < menuItems.Count; i++)
            {
                if(selectedIndex == i)
                {
                    spriteBatch.DrawString(hilightFont, menuItems[i], tempPos, hilightColor);
                    tempPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Checks for keyboard inport
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if(ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if(selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if(ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if(selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count-1;
                }
            }
            oldState = ks;

            base.Update(gameTime);
        }
    }
}
