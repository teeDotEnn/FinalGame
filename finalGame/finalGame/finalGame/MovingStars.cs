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
    public class MovingStars : MyObject
    {
        private Vector2 position, position2;
        private int delay;
        private int delayCounter;

        public MovingStars(Game game, SpriteBatch spriteBatch, Vector2 speed, Vector2 position) : base(game)
        {
            SpriteBatch = spriteBatch;
            Tex = game.Content.Load<Texture2D>("Images/starsLight");
            this.position = position;
            position2 = new Vector2(position.X + Tex.Width, position.Y);
            Speed = speed;
            delay = 2;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Tex, position, Color.White);
            SpriteBatch.Draw(Tex, position2, Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if(delayCounter > delay)
            {
                position -= Speed;
                position2 -= Speed;
                delayCounter = 0;
            }    
            
            if (position.X < -Tex.Width)
            {
                position.X = position2.X + Tex.Width;
            }
            if (position2.X < -Tex.Width)
            {
                position2.X = position.X + Tex.Width;
            }

            base.Update(gameTime);
        }
    }
}
