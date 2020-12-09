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
    public abstract class MyObject : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        
        public Vector2 Position { get => position; set => position = value; }
        public Texture2D Tex { get => tex; set => tex = value; }
        public SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }
        public Vector2 Speed { get => speed; set => speed = value; }

        public MyObject(Game game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
