﻿using System;
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
    public class ActionScene : GameScene
    {
        private Ship ship;
        private List<Alien> alienList = new List<Alien>();
        private Texture2D alienTex;
        private Bullet bullet;
        private List<Bullet> ourBulletsList = new List<Bullet>();
        private Texture2D bulletTex;
        private Game1 game;
        private CollisionManager collisionManager;
        private CollisionManager collisionManagerEnemy;
        private SpriteFont font;
        public CollisionManager CollisionManager { get => collisionManager; set => collisionManager = value; }
        int delay = 22; //22
        int delayCounter = 0;
        int highscore;
        bool dead = false;
        public bool Dead { get => dead; set => dead = value; }
        public int Highscore { get => highscore; set => highscore = value; }
        private int level = 1;
        private int levelDelay = 200;
        private int levelDelayCounter = 0;
        private bool levelDone = false;
        private Asteroid asteroid;
        private AsteroidCollisionManager asteroidCollisionManager;
        private int asteroidDelay = 250;
        private int asteroidDelayCounter = 200;

        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;
            this.game = (Game1)game;
            font = game.Content.Load<SpriteFont>("Fonts/HelpFont");



            Texture2D shipTex = game.Content.Load<Texture2D>("Images/shipCropped");
            ship = new Ship(game, SpriteBatch, shipTex);
            this.Components.Add(ship);

            alienTex = game.Content.Load<Texture2D>("Images/spaceInvaderGreen");
            // create a update method for creating aliens and adding them to the list

            /*
              OLD ALIEN CODE
             alien = new Alien(game, SpriteBatch, alienTex, new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2));
               
            this.Components.Add(alien);
            alienList.Add(alien);
            */
            alienList = generateAliens(level);
            foreach(Alien alien in alienList)
            {
                this.Components.Add(alien);
            }
            //This needs to go here
            collisionManager = new CollisionManager(game, spriteBatch, ourBulletsList, alienList, ship);
            collisionManagerEnemy = new CollisionManager(game, spriteBatch, alienList, ship);
            asteroidCollisionManager = new AsteroidCollisionManager(game, spriteBatch, ship);
            this.Components.Add(collisionManager);
            Components.Add(collisionManagerEnemy);
            Components.Add(asteroidCollisionManager);

            bulletTex = game.Content.Load<Texture2D>("Images/bulletYellowSmall");
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

        public override void Draw(GameTime gameTime)
        {
            //List<int> highscoreList
            
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
            //shooting(THIS IS FUCKED AND NEEDS TO BE REFACTORED)
            
            alienList = collisionManager.AlienList;
            ourBulletsList = collisionManager.BulletList;

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Space) && delayCounter > delay)
            {
                bullet = new Bullet(game, SpriteBatch, bulletTex, new Vector2(ship.Position.X + ship.Tex.Width / 2, ship.Position.Y - bulletTex.Height));
                this.Components.Add(bullet);
                ourBulletsList.Add(bullet);
                delayCounter = 0;
            }

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
            Console.WriteLine(alienList.Count);
            Console.WriteLine(levelDone);

            collisionManager.AlienList = alienList;
            collisionManager.BulletList = ourBulletsList;
            collisionManagerEnemy.AlienList = alienList;

            delayCounter++;
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

            if (File.Exists(game.Filepath))
            {
                string[] linesFromFile = File.ReadAllLines(game.Filepath);
                if(linesFromFile.Length>0)
                {
                    string[] fields = linesFromFile[0].Split('|');
                    highscore = int.Parse(fields[1]);
                }
            }
            if(collisionManager.Score > highscore)
            {
                highscore = collisionManager.Score;
            }

            if (ship.Enabled == false)
            {
                dead = true;
            }

            if(level > 2)
            {
                if(asteroidDelayCounter > asteroidDelay)
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
       

        public void myMute()
        {
            collisionManager.myMute();
            collisionManagerEnemy.myMute();
            asteroidCollisionManager.myMute();
        }

        public int score()
        {
            return collisionManager.Score;
        }
    }
}
