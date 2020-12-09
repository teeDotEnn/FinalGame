using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace finalGame
{
    public class CollisionManager : GameComponent
    {
        private SpriteBatch spriteBatch;
        private List<Bullet> bulletList;
        private List<Alien> alienList;
        private Ship ship;
        private Explosion explosion;
        private Game game;
        private Texture2D explosionTex;

        public List<Bullet> BulletList { get => bulletList; set => bulletList = value; }
        public List<Alien> AlienList { get => alienList; set => alienList = value; }

        public CollisionManager(Game game, SpriteBatch spriteBatch, List<Bullet> bulletList, List<Alien> alienList) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.game = game;
            this.bulletList = bulletList;
            this.alienList = alienList;

            explosionTex = game.Content.Load<Texture2D>("Images/explosion");
            
            //game.Components.Add()
        }

        public void hide()
        {
            if (explosion != null)
            {
                explosion.Visible = false;
                explosion.Enabled = false;
            }
        }
        

        public override void Update(GameTime gameTime)
        {
            foreach (Bullet bullet in bulletList)
            {
                Rectangle bulletRect = bullet.getBound();
                foreach (Alien alien in alienList)
                {
                    Rectangle alienRect = alien.getBound();
                    if(bulletRect.Intersects(alienRect))
                    {
                        Console.WriteLine("You hit!");
                        alien.Enabled = false;
                        alien.Visible = false;
                        bullet.Enabled = false;
                        bullet.Visible = false;
                        BulletList.Remove(bullet);
                        AlienList.Remove(alien);

                        explosion = new Explosion(game, spriteBatch, explosionTex, alien.Position);
                        game.Components.Add(explosion);
                        return;

                    }
                }

            }
            
            
            base.Update(gameTime);
        }
    }
}
