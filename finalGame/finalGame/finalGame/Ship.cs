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
    public class Ship : MyObject
    {
        
        public Ship(Game game, SpriteBatch spriteBatch, Texture2D tex) : base(game)
        {
            SpriteBatch = spriteBatch;
            Tex = tex;
            Position = new Vector2(Shared.stage.X / 2 - tex.Width / 2, Shared.stage.Y - tex.Height);
            Speed = new Vector2(7, 0);
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
            KeyboardState ks = Keyboard.GetState();
            if(ks.IsKeyDown(Keys.Right))
            {
                Position += Speed;
                if(Position.X > (Shared.stage.X - Tex.Width))
                {
                    //Position.X = Shared.stage.X - Tex.Width;
                    Position = new Vector2(Shared.stage.X - Tex.Width, Shared.stage.Y - Tex.Height);
                }
            }
            if(ks.IsKeyDown(Keys.Left))
            {
                Position -= Speed;
                if(Position.X < 0)
                {
                    Position = new Vector2(0, Shared.stage.Y - Tex.Height);
                }
            }

            base.Update(gameTime);
        }


    }
}
