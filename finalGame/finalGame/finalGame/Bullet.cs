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
        public Bullet(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game)
        {
            SpriteBatch = spriteBatch;
            Tex = tex;
            Position = position;
            Speed = new Vector2(0,7);
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
            Position -= Speed;
            if(Position.Y < 0)
            {
                this.Enabled = false;
            }
            base.Update(gameTime);
        }

        public Rectangle getBound()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Tex.Width, Tex.Height);
        }
    }
}
