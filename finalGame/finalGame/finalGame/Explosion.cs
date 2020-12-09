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
    public class Explosion : MyObject
    {
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        private const int ROW = 5;
        private const int COL = 5;

        public Explosion(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game)
        {
            SpriteBatch = spriteBatch;
            Tex = tex;
            Position = position;
            delay = 3;

            dimension = new Vector2(tex.Width / COL, tex.Height / ROW);
            createFrames();
        }

        public void hide()
        {
            Visible = false;
            Enabled = false;
        }

        public void start()
        {
            frameIndex = -1;
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
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            if(frameIndex >= 0)
            {
                SpriteBatch.Draw(Tex, Position, frames[frameIndex], Color.White);
            }
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if(delayCounter > delay)
            {
                frameIndex++;
                if(frameIndex > ROW*COL-1)
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
