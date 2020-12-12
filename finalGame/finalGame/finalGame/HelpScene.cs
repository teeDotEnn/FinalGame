/* File: HelpScene.cs
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
    /// Renders the help scene
    /// </summary>
    public class HelpScene : GameScene
    {
        private Vector2 position;
        private SpriteFont font;
        private SpriteFont fontHead;
        private Vector2 headPosition = new Vector2(25, 50);
        private Vector2 bodyPosition = new Vector2(25, 120);
        private int lineDisplacement;

        /// <summary>
        /// Instatiates a new help scene
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        public HelpScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;
            font = game.Content.Load<SpriteFont>("Fonts/HelpFont");
            fontHead = game.Content.Load<SpriteFont>("Fonts/HelpFontBold");
            position = new Vector2(50, 50);
        }
        /// <summary>
        /// Draws the help
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            lineDisplacement = font.LineSpacing + 10;
            SpriteBatch.Begin();
            SpriteBatch.DrawString(fontHead, "Help", headPosition, Color.White);
            SpriteBatch.DrawString(font, "Your goal is to defeat all of the aliens and save the planet!", bodyPosition, Color.White);
            SpriteBatch.DrawString(font, "Use the arrow keys to move your ship back and forth.", new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement)), Color.White);
            SpriteBatch.DrawString(font, "You can shoot by pressing the spacebar.", new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement*2)), Color.White);
            SpriteBatch.DrawString(font, "Be careful to dodge the enemy bullets and ships!", new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement * 3)), Color.White);
            SpriteBatch.DrawString(font, "You are the Earth's last line of defence.", new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement * 5)), Color.White);
            SpriteBatch.DrawString(font, "No pressure.", new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement * 8)), Color.White);
            SpriteBatch.DrawString(font, "Press Esc to return.", new Vector2(Shared.stage.X - 300, Shared.stage.Y - 50), Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
