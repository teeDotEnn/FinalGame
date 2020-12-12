/* File: GameScene.cs
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
    /// used to build other game scenes
    /// </summary>
    public abstract class GameScene : DrawableGameComponent
    {
        private List<GameComponent> components;
        /// <summary>
        /// The components of the scene
        /// </summary>
        public List<GameComponent> Components { get => components; set => components = value; }
        
        private SpriteBatch spriteBatch;
        /// <summary>
        /// The SpriteBatch
        /// </summary>
        public SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }
        /// <summary>
        /// Show the scene
        /// </summary>
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }
        /// <summary>
        /// hide the scene
        /// </summary>
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        protected GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            hide();
        }
        /// <summary>
        /// Draw the components in the game scene's list
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in components)
            {
                if(item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if(comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }

            }
            
            
            base.Draw(gameTime);
        }
        /// <summary>
        /// Update enabled components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if(item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }
    }
}
