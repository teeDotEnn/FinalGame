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
    public class Asteroid : MyObject
    {
        public Asteroid(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game)
        {
            Tex = game.Content.Load<Texture2D>("Images/meteorSmall");
            SpriteBatch = spriteBatch;
            Position = position;
            Speed = new Vector2(0, 10);
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
