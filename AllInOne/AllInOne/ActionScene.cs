using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllInOne
{
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Bat bat;
        public ActionScene(Game game,SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            Texture2D batTex = game.Content.Load<Texture2D>("Images/Bat");
            bat = new Bat(game, spriteBatch, batTex);
            this.Components.Add(bat);
        }
    }
}
