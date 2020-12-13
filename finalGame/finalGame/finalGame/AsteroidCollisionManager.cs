/* File Name: AsteroidCollisionManager.cs
 * Purpose: checks for collisions between asteroids and the ship
 * Rev History: Created 2020-12-10
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
    /// THe collision manager for handling asteroids
    /// </summary>
    public class AsteroidCollisionManager : GameComponent
    {
        private Game1 game;
        private SpriteBatch spriteBatch;
        private Ship ship;
        private Explosion explosion;
        private SoundEffect explosionSound;
        private Texture2D explosionTex;
        private List<Asteroid> asteroidList = new List<Asteroid>();
        /// <summary>
        /// The list of asteroids
        /// </summary>
        public List<Asteroid> AsteroidList { get => asteroidList; set => asteroidList = value; }
        private bool mute = false;
        /// <summary>
        /// Instatiates a new instance of the collision manager
        /// </summary>
        /// <param name="game">The game that hosts the collision manager</param>
        /// <param name="spriteBatch">The sprite batch to draw the explosions</param>
        /// <param name="ship">The player's ship</param>
        public AsteroidCollisionManager(Game game, SpriteBatch spriteBatch, Ship ship) : base(game)
        {
            this.game = (Game1)game;
            this.spriteBatch = spriteBatch;
            this.ship = ship;



            explosionTex = game.Content.Load<Texture2D>("Images/explosion");
            explosionSound = game.Content.Load<SoundEffect>("Sounds/explosionSound");
        }

        
        /// <summary>
        /// Checks to see if the ship is hit by an asteroid
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Rectangle shipRect = ship.getBound();
            if(asteroidList.Count > 0)
            {
                foreach (Asteroid asteroid in asteroidList)
                {
                    Rectangle asteroidRect = asteroid.getBound();
                    if (asteroidRect.Intersects(shipRect))
                    {
                        ship.Enabled = false;
                        ship.Visible = false;
                        explosion = new Explosion(game, spriteBatch, explosionTex, ship.Position);
                        if (!mute)
                        {
                            explosionSound.Play(.1f, 0.0f, 0.0f);
                        }
                        game.Components.Add(explosion);
                    }
                }
            }
            
            base.Update(gameTime);
        }
        /// <summary>
        /// prevents the sound from being played
        /// </summary>
        public void myMute()
        {
            mute = !mute;
        }
    }
}
