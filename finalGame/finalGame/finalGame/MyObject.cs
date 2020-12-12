/* File: MyObject.cs
 * Purpose: The object for drawable game components
 * Rev History:
 *          Created 2020-12-08
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
    /// The base class for a drawable compontent
    /// </summary>
    public abstract class MyObject : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        /// <summary>
        /// Position of the object
        /// </summary>
        public Vector2 Position { get => position; set => position = value; }
        /// <summary>
        /// The texture of the object
        /// </summary>
        public Texture2D Tex { get => tex; set => tex = value; }
        /// <summary>
        /// The spritebatch to draw the object
        /// </summary>
        public SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }
        /// <summary>
        /// THe speed of the object
        /// </summary>
        public Vector2 Speed { get => speed; set => speed = value; }
        /// <summary>
        /// Constructor for the object
        /// </summary>
        /// <param name="game">The game object that owns the object</param>
        public MyObject(Game game) : base(game)
        {
        }
        /// <summary>
        /// Draws the object
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        /// <summary>
        /// Update the object
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        /// <summary>
        /// Get the boundaries of the object
        /// </summary>
        /// <returns></returns>
        public Rectangle getBound()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Tex.Width, Tex.Height);
        }

    }
}
