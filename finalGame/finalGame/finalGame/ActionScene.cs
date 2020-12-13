/* File Name: ActionScene.cs
 * Purpose: To serve as teh main class that handles our gameplay
 * Rev History: Created 2020-12-8
 *              By Stephen Draper
 *              Modified by Stephen Draper and Timothy Nigh
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace finalGame
{
    /// <summary>
    /// The class to hold the action scene
    /// </summary>
    public class ActionScene : GameScene
    {
        //Resources
        private Ship ship;
        private List<Alien> alienList = new List<Alien>();
        private Texture2D alienTex;
        private Bullet bullet;
        private List<Bullet> ourBulletsList = new List<Bullet>();
        private Texture2D bulletTex;
        private Game1 game;
        private Asteroid asteroid;
        private SpriteFont font;

        //Collision managers
        private CollisionManager collisionManager;
        private CollisionManager collisionManagerEnemy;
        private AsteroidCollisionManager asteroidCollisionManager;
        /// <summary>
        /// Property for the collision manager that handles friendly bullets vs enemies
        /// </summary>
        public CollisionManager CollisionManager { get => collisionManager; set => collisionManager = value; }
        //Delays for firing
        int delay = 22; //22
        int delayCounter = 0;
        int highscore;
        //Flag for if the player has died or not
        bool dead = false;
        /// <summary>
        /// Flag for if the player has died or not
        /// </summary>
        public bool Dead { get => dead; set => dead = value; }
        public int Highscore { get => highscore; set => highscore = value; }
        private int level = 1;
        private int levelDelay = 200;
        private int levelDelayCounter = 0;
        private bool levelDone = false;
        
        //Delays for dropping asteroids
        private int asteroidDelay = 250;
        private int asteroidDelayCounter = 200;
        /// <summary>
        /// Instatiates a new action scene
        /// </summary>
        /// <param name="game">The game that is being created</param>
        /// <param name="spriteBatch">the spritebatch the game is using</param>
        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;
            this.game = (Game1)game;
            font = game.Content.Load<SpriteFont>("Fonts/HelpFont");



            Texture2D shipTex = game.Content.Load<Texture2D>("Images/shipCropped");
            alienTex = game.Content.Load<Texture2D>("Images/spaceInvaderGreen");
            bulletTex = game.Content.Load<Texture2D>("Images/bulletYellowSmall");


            ship = new Ship(game, SpriteBatch, shipTex);
            this.Components.Add(ship);
                        
            //Create the first level of aliens
            alienList = generateAliens(level);
            foreach(Alien alien in alienList)
            {
                this.Components.Add(alien);
            }
            
            //Instatiate collision managers
            collisionManager = new CollisionManager(game, spriteBatch, ourBulletsList, alienList, ship);
            collisionManagerEnemy = new CollisionManager(game, spriteBatch, alienList, ship);
            asteroidCollisionManager = new AsteroidCollisionManager(game, spriteBatch, ship);
            Components.Add(collisionManager);
            Components.Add(collisionManagerEnemy);
            Components.Add(asteroidCollisionManager);

            
        }

        private List<Alien> generateAliens(int level)
        {
            Random random = new Random();
            List<Alien> aliens = new List<Alien>();
            Vector2 location = new Vector2(Shared.stage.X / 4, Shared.stage.Y / 8);
            for (int i = 0; i < 24; i++)
            {
                //If the length of the rows change, we need to change thse as well
                if(i < 6 || i>11 && i < 18)
                {
                    aliens.Add(new Alien(game, SpriteBatch, alienTex, location,true, level, random));
                }
                else
                {
                    aliens.Add(new Alien(game, SpriteBatch, alienTex, location, false, level, random));
                }
                
                //Need a better way to increment rows (this controls the length of the rows)
                if(i == 5 || i == 11 || i == 17)
                {
                    location.X = (Shared.stage.X / 4);
                    location.Y = (location.Y + alienTex.Height + 10);
                }
                else
                {
                    location.X = location.X + alienTex.Width + random.Next(24,40);
                }
                
                
            }
            return aliens;
        }
        /// <summary>
        /// Draws 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            //List<int> highscoreList
            //Handles the drawing of UI elements: score, high score, level complete
            SpriteBatch.Begin();
            SpriteBatch.DrawString(font, $"Score: {collisionManager.Score}", new Vector2(20, 10), Color.White);
            SpriteBatch.DrawString(font, $"Highscore: {highscore}", new Vector2(Shared.stage.X - 250, 10), Color.White);
            if(levelDone)
            {
                Console.WriteLine("working");
                SpriteBatch.DrawString(font, $"LEVEL COMPLETE!", new Vector2(Shared.stage.X/2-130, Shared.stage.Y/2-100), Color.White);
                SpriteBatch.DrawString(font, $"Starting level {level+1}.", new Vector2(Shared.stage.X / 2 - 105, Shared.stage.Y/2-50), Color.White);
            }
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            
            //Get the lists of aliens and bullets from the collision manager
            alienList = collisionManager.AlienList;
            
            //check to see if all aliens are dead, and if the level needs to be incremented
            if (alienList.Count == 0)
            {
                levelDone = true;
                levelDelayCounter++;
                if(levelDelayCounter > levelDelay)
                {
                    levelDone = false;
                    level++;
                    alienList = generateAliens(level);
                    foreach (Alien alien in alienList)
                    {
                        Components.Add(alien);
                    }
                    levelDelayCounter = 0;
                }
            }
           //Update collision manager lists
            collisionManager.AlienList = alienList;
            collisionManagerEnemy.AlienList = alienList;

            
            //remove dead compoments
            int count = Components.Count;
            for (int i = 0; i < count; i++)
            {
                if (Components[i].Enabled == false)
                {
                    Components.RemoveAt(i);
                    count--;
                    i--;
                }
            }
            //Get the highscore from file

            if (File.Exists(game.Filepath))
            {
                string[] linesFromFile = File.ReadAllLines(game.Filepath);
                if(linesFromFile.Length>0)
                {
                    string[] fields = linesFromFile[0].Split('|');
                    highscore = int.Parse(fields[1]);
                }
            }

            //Check to see if the player's score is higher than the high score
            if(collisionManager.Score > highscore)
            {
                highscore = collisionManager.Score;
            }
            
            //Check to see if the player has died
            if (ship.Enabled == false)
            {
                dead = true;
            }

            //Fire asteroids at the player on level 3+
            if(level > 2)
            {
                if(asteroidDelayCounter > asteroidDelay && levelDone == false)
                {
                    asteroid = new Asteroid(game, SpriteBatch, new Vector2(ship.Position.X, 0));
                    asteroidCollisionManager.AsteroidList.Add(asteroid);
                    Components.Add(asteroid);
                    asteroidDelayCounter = 0;
                }
                asteroidDelayCounter++;
            }

            base.Update(gameTime);
        }
       
        /// <summary>
        /// Used to mute all collision managers
        /// </summary>
        public void myMute()
        {
            collisionManager.myMute();
            collisionManagerEnemy.myMute();
            asteroidCollisionManager.myMute();
        }
        /// <summary>
        /// gets the score from the collision manager
        /// </summary>
        /// <returns>The player's score</returns>
        public int score() => collisionManager.Score;
        
    }
}
