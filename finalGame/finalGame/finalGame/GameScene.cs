﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace finalGame
{
    public abstract class GameScene : DrawableGameComponent
    {
        private List<GameComponent> components;
        public List<GameComponent> Components { get => components; set => components = value; }
        
        private SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }

        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

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
