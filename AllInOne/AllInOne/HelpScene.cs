﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllInOne
{
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public HelpScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/helpImage");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
