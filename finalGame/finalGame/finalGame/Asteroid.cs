/* File Name: Asteroid.cs
 * Purpose: Contains logic for asteroids
 * Rev History: Created 2020-12-10
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
    /// Asteroid class
    /// </summary>
    public class Asteroid : MyObject
    {
        /// <summary>
        /// The constructor for the asteroid object
        /// </summary>
        /// <param name="game">The game object to hold the asteroid</param>
        /// <param name="spriteBatch">the sprite batch</param>
        /// <param name="position">the position at which the asteroid is created</param>
        public Asteroid(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game)
        {
            Tex = game.Content.Load<Texture2D>("Images/meteorSmall");
            SpriteBatch = spriteBatch;
            Position = position;
            Speed = new Vector2(0, 10);
        }
        /// <summary>
        /// Draw the asteroid at its position
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Tex, Position, Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Increment the position of the asteroid
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Position += Speed;
            if(Position.X > Shared.stage.X)
            {
                this.Enabled = false;
                this.Visible = false;
            }
            base.Update(gameTime);
        }
    }
}
