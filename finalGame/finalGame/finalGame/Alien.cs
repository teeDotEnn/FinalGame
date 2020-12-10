/* File Name: Alien.cs
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
    /// The Alien Object
    /// </summary>
    public class Alien : MyObject
    {
        /// <summary>
        /// Instatiates a new alien object
        /// </summary>
        /// <param name="game">The game object</param>
        /// <param name="spriteBatch">The scene's spritebatch</param>
        /// <param name="tex">The 2d texture for the alien</param>
        /// <param name="position">The position </param>
        public Alien(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, bool moveLeft) : base(game)
        {
            SpriteBatch = spriteBatch;
            Tex = tex;
            Position = position;
            // TO DO 
            if (moveLeft)
            {
                Speed = new Vector2(0, -4);
            }
            else
            {
                Speed = new Vector2(0, 4);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Tex, Position, Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 position = Position;
            position.Y += Speed.Y;
            base.Update(gameTime);
        }

        public Rectangle getBound()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Tex.Width, Tex.Height);
        }
    }
}
