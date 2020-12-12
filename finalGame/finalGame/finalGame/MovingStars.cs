/* File: MovingStars.cs
 * Purpose: To give the background a parallax effect
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
    /// The background class for our game
    /// </summary>
    public class MovingStars : MyObject
    {
        private Vector2 position, position2;
        private int delay;
        private int delayCounter;
        /// <summary>
        /// Instiate a new background
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="speed"></param>
        /// <param name="position"></param>
        public MovingStars(Game game, SpriteBatch spriteBatch, Vector2 speed, Vector2 position) : base(game)
        {
            SpriteBatch = spriteBatch;
            Tex = game.Content.Load<Texture2D>("Images/starsLight");
            this.position = position;
            position2 = new Vector2(position.X + Tex.Width, position.Y);
            Speed = speed;
            delay = 2;
        }
        /// <summary>
        /// Draw the backgrounds
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Tex, position, Color.White);
            SpriteBatch.Draw(Tex, position2, Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Move the backgrounds
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if(delayCounter > delay)
            {
                position -= Speed;
                position2 -= Speed;
                delayCounter = 0;
            }    
            
            if (position.X < -Tex.Width)
            {
                position.X = position2.X + Tex.Width;
            }
            if (position2.X < -Tex.Width)
            {
                position2.X = position.X + Tex.Width;
            }

            base.Update(gameTime);
        }
    }
}
