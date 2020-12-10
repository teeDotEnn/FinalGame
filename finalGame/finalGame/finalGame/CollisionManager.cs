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
        private SoundEffect explosionSound;
        private bool mute = false;
        private List<Explosion> explosionList = new List<Explosion>();

        public List<Bullet> BulletList { get => bulletList; set => bulletList = value; }
        public List<Alien> AlienList { get => alienList; set => alienList = value; }

        public CollisionManager(Game game, SpriteBatch spriteBatch, List<Bullet> bulletList, List<Alien> alienList) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.game = game;
            this.bulletList = bulletList;
            this.alienList = alienList;

            explosionTex = game.Content.Load<Texture2D>("Images/explosion");
            explosionSound = game.Content.Load<SoundEffect>("Sounds/explosionSound");
            
            
            //game.Components.Add()
        }

        public void hide()
        {
            if (explosionList.Count > 0)
            {
                foreach (var item in explosionList)
                {
                    item.Enabled = false;
                    item.Visible = false;
                }
            }
        }

        public void myMute()
        {
            mute = !mute;
        }


        public override void Update(GameTime gameTime)
        {
            //Manage bullets
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
                        explosionList.Add(explosion);
                        if(!mute)
                        {
                            explosionSound.Play(.1f,0.0f,0.0f);
                        }
                        game.Components.Add(explosion);
                        return;

                    }
                }

                //Manage aliens

                

            }
            

            // Our list is for the purpose of making explosions invisible when we pause. However, we don't want it to build up and cause
            // memory issues. This checks if the most recent explosion is still visible, and if it is not, clears the entire list. This will
            // happen frequently enough that memory will not be an issue.
            if(explosion != null && !explosion.Visible)
            {
                explosionList.Clear();
            }
            
            base.Update(gameTime);
        }
    }
}
