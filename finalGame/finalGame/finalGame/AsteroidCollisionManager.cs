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
    class AsteroidCollisionManager : GameComponent
    {
        private Game1 game;
        private SpriteBatch spriteBatch;
        private Ship ship;
        private Explosion explosion;
        private SoundEffect explosionSound;
        private Texture2D explosionTex;
        private List<Asteroid> asteroidList = new List<Asteroid>();
        public List<Asteroid> AsteroidList { get => asteroidList; set => asteroidList = value; }
        private bool mute = false;

        public AsteroidCollisionManager(Game game, SpriteBatch spriteBatch, Ship ship) : base(game)
        {
            this.game = (Game1)game;
            this.spriteBatch = spriteBatch;
            this.ship = ship;



            explosionTex = game.Content.Load<Texture2D>("Images/explosion");
            explosionSound = game.Content.Load<SoundEffect>("Sounds/explosionSound");
        }

        

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

        public void myMute()
        {
            mute = !mute;
        }
    }
}
