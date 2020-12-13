/* File Name: Bullet.cs
 * Purpose: Contains logic for bullets
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
    /// The class that storees bullets
    /// </summary>
    public class Bullet : MyObject
    {
        private bool isAlien;

        /// <summary>
        /// instatiates a new bullet
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">spritebatch</param>
        /// <param name="tex">texture to be used for the bullet</param>
        /// <param name="position">starting positon of the bullet</param>
        public Bullet(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game)
        {
            SpriteBatch = spriteBatch;
            Tex = tex;
            Position = position;
            Speed = new Vector2(0,7);
        }

        /// <summary>
        /// instatiates a new bullet
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">spritebatch</param>
        /// <param name="tex">texture to be used for the bullet</param>
        /// <param name="position">starting positon of the bullet</param
        /// <param name="isAlien">boolean to determine if the bullet belongs to an alien or not</param>
        public Bullet(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, bool isAlien) : base(game)
        {
            SpriteBatch = spriteBatch;
            Tex = tex;
            Position = position;
            Speed = new Vector2(0, 7);
            this.isAlien = isAlien;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Tex, Position, Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Updates the position of the bullet
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if(!isAlien)
            {
                Position -= Speed;
                if (Position.Y < 0)
                {
                    this.Enabled = false;
                    this.Visible = false;
                }
            }
            else
            {
                Position += Speed;
                if (Position.Y > Shared.stage.Y)
                {
                    this.Enabled = false;
                    this.Visible = false;
                }
            }
            
            base.Update(gameTime);
        }
    }
}
