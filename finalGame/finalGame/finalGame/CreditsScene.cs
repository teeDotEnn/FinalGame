/* File Name: CreditsScene.cs
 * Purpose: Contains logic for aliens in the game
 * Rev History: Created 2020-12-08
 *                  Stephen Draper
 * 
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
    /// The scene for credits
    /// </summary>
    public class CreditsScene : GameScene
    {
        private SpriteFont font;
        private SpriteFont fontHead;
        private Vector2 headPosition = new Vector2(25, 50);
        private Vector2 bodyPosition = new Vector2(25, 120);
        private int lineDisplacement;

        /// <summary>
        /// instatiates a scene
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="spriteBatch">the spritebatch</param>
        public CreditsScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;
            font = game.Content.Load<SpriteFont>("Fonts/HelpFont");
            fontHead = game.Content.Load<SpriteFont>("Fonts/HelpFontBold");
        }
        /// <summary>
        /// Draws the credits
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            lineDisplacement = font.LineSpacing + 10;
            SpriteBatch.Begin();
            SpriteBatch.DrawString(fontHead, "Credits", headPosition, Color.White);
            SpriteBatch.DrawString(font, "Creators:      Stephen Draper & Tim Nigh", bodyPosition, Color.White);
            SpriteBatch.DrawString(font, "Explosion:    \"Explosion, 8 - bit, 01.wav\" by InspectorJ", new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement)), Color.White);
            SpriteBatch.DrawString(font, "Music:           Drakensson", new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement*2)), Color.White);
            SpriteBatch.DrawString(font, "Sprites:         See Word Doc!", new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement * 3)), Color.White);
            SpriteBatch.DrawString(font, "Press Esc to return.", new Vector2(Shared.stage.X - 300, Shared.stage.Y - 50), Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
