using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimatedExplosion
{
    public class Explosion : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        private const int ROW = 5;
        private const int COL = 5;

        public Vector2 Position { get => position; set => position = value; }

        public Explosion(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.delay = delay;

            dimension = new Vector2(tex.Width / COL, tex.Height / ROW);

            hide();

            // create frames
            createFrames();


        }

        public void hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }

        public void start()
        {
            frameIndex = -1;
            this.Visible = true;
            this.Enabled = true;
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();

            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle rect = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(rect);
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if(frameIndex >= 0)
            {
                spriteBatch.Draw(tex, Position, frames[frameIndex], Color.White);
            }
            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROW * COL - 1)
                {
                    frameIndex = -1;
                    hide();
                }
                delayCounter = 0;
            }

            base.Update(gameTime);
        }
    }
}
