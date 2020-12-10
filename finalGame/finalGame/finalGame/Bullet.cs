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
    public class Bullet : MyObject
    {
        private bool isAlien;
        public Bullet(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game)
        {
            SpriteBatch = spriteBatch;
            Tex = tex;
            Position = position;
            Speed = new Vector2(0,7);
        }

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

        public override void Update(GameTime gameTime)
        {
            if(!isAlien)
            {
                Position -= Speed;
                if (Position.Y < 0)
                {
                    this.Enabled = false;
                }
            }
            else
            {
                Position += Speed;
                if (Position.Y > Shared.stage.Y)
                {
                    this.Enabled = false;
                }
            }
            
            base.Update(gameTime);
        }
    }
}
