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
    public class ActionScene : GameScene
    {
        private Ship ship;
        private Alien alien;

        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;
            
            Texture2D shipTex = game.Content.Load<Texture2D>("Images/shipCropped");
            ship = new Ship(game, SpriteBatch, shipTex);
            this.Components.Add(ship);

            Texture2D alienTex = game.Content.Load<Texture2D>("Images/spaceInvaderGreen");
            alien = new Alien(game, SpriteBatch, alienTex, new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2));
            this.Components.Add(alien);
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
