/* File Name: CollisionManager.cs
 * Purpose: Handles collisions between aliens, ships and their bullets
 * Rev History: Created 2020-12-08
 *                  Stephen Draper
 * 
 */
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
    /// <summary>
    /// The main collision manager
    /// </summary>
    public class CollisionManager : GameComponent
    {
        private SpriteBatch spriteBatch;
        //private List<Bullet> bulletList;
        private List<Alien> alienList;
        private Ship ship;
        private Explosion explosion;
        private Game game;
        private Texture2D explosionTex;
        private SoundEffect explosionSound;
        private bool mute = false;
        private List<Explosion> explosionList = new List<Explosion>();
        private int score;
        /// <summary>
        /// The list of bullets
        /// </summary>
        //public List<Bullet> BulletList { get => bulletList; set => bulletList = value; }
        /// <summary>
        /// The list of aliens
        /// </summary>
        public List<Alien> AlienList { get => alienList; set => alienList = value; }
        public int Score { get => score; set => score = value; }
        
        /// <summary>
        /// Instatiate a new collision manager
        /// </summary>
        /// <param name="game">The game that the collision manager is managing</param>
        /// <param name="spriteBatch">The the spriteBatch</param>
        /// <param name="bulletList">the bullet list</param>
        /// <param name="alienList">the alient list</param>
        /// <param name="ship">the ship</param>
        public CollisionManager(Game game, SpriteBatch spriteBatch, List<Bullet> bulletList, List<Alien> alienList, Ship ship) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.game = game;
            //this.bulletList = ship.BulletList;
            this.alienList = alienList;
            this.ship = ship;

            explosionTex = game.Content.Load<Texture2D>("Images/explosion");
            explosionSound = game.Content.Load<SoundEffect>("Sounds/explosionSound");
            
            
            //game.Components.Add()
        }
        /// <summary>
        /// Instatiate a new collision manager, but with only aliens and the ship
        /// </summary>
        /// <param name="game">The game that the collision manager is managing</param>
        /// <param name="spriteBatch">The the spriteBatch</param>
        /// <param name="alienList">the alient list</param>
        /// <param name="ship">the ship</param>
        public CollisionManager(Game game, SpriteBatch spriteBatch, List<Alien> alienList, Ship ship) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.game = game;
            this.alienList = alienList;
            this.ship = ship;

            explosionTex = game.Content.Load<Texture2D>("Images/explosion");
            explosionSound = game.Content.Load<SoundEffect>("Sounds/explosionSound");


            //game.Components.Add()
        }
        /// <summary>
        /// Hides explosions
        /// </summary>
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
        /// <summary>
        /// Mutes explosions
        /// </summary>
        public void myMute()
        {
            mute = !mute;
        }

        /// <summary>
        /// Checks to see if bullets are colliding with aliens or with the player
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            
            foreach (Alien alien in alienList)
            {
                Rectangle alienRect = alien.getBound();
                // manages players bullets
                if(ship.BulletList.Count != 0)
                {
                    Console.WriteLine($"Ship bullet count: {ship.BulletList.Count}");
                    foreach (Bullet bullet in ship.BulletList)
                    {
                        Rectangle bulletRect = bullet.getBound();
                        if (bulletRect.Intersects(alienRect))
                        {
                            Console.WriteLine("You hit!");
                            alien.Enabled = false;
                            alien.Visible = false;
                            bullet.Enabled = false;
                            bullet.Visible = false;
                            ship.BulletList.Remove(bullet);
                            AlienList.Remove(alien);

                            explosion = new Explosion(game, spriteBatch, explosionTex, alien.Position);
                            explosionList.Add(explosion);
                            if (!mute)
                            {
                                explosionSound.Play(.1f, 0.0f, 0.0f);
                            }
                            game.Components.Add(explosion);
                            Score += 10;
                            return;

                        }
                    }
                }
                else
                {
                    //Manages alien bullets
                    Rectangle shipRect = ship.getBound();
                    if(alien.BulletList != null)
                    {
                        foreach (Bullet bullet in alien.BulletList)
                        {
                            Rectangle bulletRect = bullet.getBound();
                            if (bulletRect.Intersects(shipRect))
                            {
                                ship.Enabled = false;
                                ship.Visible = false;
                                explosion = new Explosion(game, spriteBatch, explosionTex, ship.Position);
                                explosionList.Add(explosion);
                                if (!mute)
                                {
                                    explosionSound.Play(.1f, 0.0f, 0.0f);
                                }
                                game.Components.Add(explosion);
                            }
                        }
                    }
                }
            }
            //Manage aliens
            foreach (Alien alien in alienList)
            {
                Rectangle alienRect = alien.getBound();
                Rectangle shipRect = ship.getBound();
                if(alienRect.Intersects(shipRect) && ship.Enabled == true)
                {
                    ship.Enabled = false;
                    ship.Visible = false;
                    explosion = new Explosion(game, spriteBatch, explosionTex, ship.Position);
                    explosionList.Add(explosion);
                    if(!mute)
                    {
                        explosionSound.Play(.1f, 0.0f, 0.0f);
                    }
                    game.Components.Add(explosion);
                }
            }


            // Our list is for the purpose of making explosions invisible when we pause. However, we don't want it to build up and cause
            // memory issues. This checks if the most recent explosion is still visible, and if it is not, clears the entire list. This will
            // happen frequently enough that memory will not be an issue.
            if (explosion != null && !explosion.Visible)
            {
                explosionList.Clear();
            }
            
            base.Update(gameTime);
        }
    }
}
