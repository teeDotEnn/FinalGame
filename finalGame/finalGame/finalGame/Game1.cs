using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.IO;

namespace finalGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private ActionScene actionScene;
        private StartScene startScene;
        private PauseScreen pauseScreen;
        private OptionScene optionScene;
        private HelpScene helpScene;
        private CreditsScene creditsScene;
        private HighscoreScene highscoreScene;
        private Song song;
        private string file = "highscores.txt";

        private KeyboardState oldstate;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 960;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            //background
            MovingStars stars = new MovingStars(this, spriteBatch, new Vector2(1,0), new Vector2 (0,0));
            Components.Add(stars);
            MovingStars stars2 = new MovingStars(this, spriteBatch, new Vector2(2,0), new Vector2(-50, -50));
            Components.Add(stars2);

            //pull highscore from file - create file if it doesn't exist
            List<string> nameList = new List<string>();
            List<int> highscoreList = new List<int>();
            List<string> nameDefault = new List<string>() { "TIM.....", "SMD.....", "JOE.....","KAT.....","BUT.....",
                                                                 "CAR.....", "RIP.....", "MOM.....", "COO.....", "MEE....."};
            List<int> highscoreDefault = new List<int>() { 999, 888, 777, 666, 555, 444, 333, 222, 111, 100 };


            if(File.Exists(file))
            {
                string[] linesFromFile = File.ReadAllLines(file);
                foreach (string line in linesFromFile)
                {
                    string[] fields = line.Split('|');
                    nameList.Add(fields[0]);
                    highscoreList.Add(int.Parse(fields[1]));
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    int highscoreIndex = 0;
                    foreach (string name in nameDefault)
                    {
                        string stringToWrite = $"{name}|{highscoreDefault[highscoreIndex]}";
                        writer.WriteLine(stringToWrite);
                        highscoreIndex++;
                    }
                }
                highscoreList = highscoreDefault;
                nameList = nameDefault;
            }


            //scenes
            actionScene = new ActionScene(this, spriteBatch);
            Components.Add(actionScene);
            startScene = new StartScene(this, spriteBatch);
            Components.Add(startScene);
            pauseScreen = new PauseScreen(this, spriteBatch);
            Components.Add(pauseScreen);
            optionScene = new OptionScene(this, spriteBatch);
            Components.Add(optionScene);
            helpScene = new HelpScene(this, spriteBatch);
            Components.Add(helpScene);
            creditsScene = new CreditsScene(this, spriteBatch);
            Components.Add(creditsScene);
            highscoreScene = new HighscoreScene(this, spriteBatch, nameList, highscoreList);
            Components.Add(highscoreScene);
            

            MediaPlayer.Volume = 10f;
            song = this.Content.Load<Song>("Sounds/AllMusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
            


            startScene.show();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            KeyboardState ks = Keyboard.GetState();
            int selectedIndex = 0;
            if(startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if(selectedIndex == 0 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    actionScene.show();
                }
                if(selectedIndex == 1 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    helpScene.show();
                }
                if(selectedIndex == 2 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    optionScene.show();
                }
                if(selectedIndex == 3 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    highscoreScene.show();
                }
                if(selectedIndex == 4 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    creditsScene.show();
                }
                if(selectedIndex == 5 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    Exit();
                }
            }
            else if(actionScene.Enabled)
            {
                if(ks.IsKeyDown(Keys.Escape))
                {
                    actionScene.hide();
                    actionScene.CollisionManager.hide();
                    pauseScreen.show();
                }
            }
            else if (helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    helpScene.hide();
                    startScene.show();
                }    
            }
            else if (optionScene.Enabled)
            {
                selectedIndex = optionScene.Menu.SelectedIndex;
                if(selectedIndex == 0 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
                    actionScene.myMute();
                }
                if(selectedIndex == 1 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    optionScene.hide();
                    startScene.show();
                }
            }
            else if(creditsScene.Enabled)
            {
                if(ks.IsKeyDown(Keys.Escape))
                {
                    creditsScene.hide();
                    startScene.show();
                }
            }
            else if (highscoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    highscoreScene.hide();
                    startScene.show();
                }
            }
            else if (pauseScreen.Enabled)
            {
                selectedIndex = pauseScreen.Menu.SelectedIndex;
                if(selectedIndex == 0 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    pauseScreen.hide();
                    actionScene.show();
                }
                if(selectedIndex == 1 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
                    actionScene.myMute();
                }
                if(selectedIndex == 2 && ks.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter))
                {
                    Components.Remove(actionScene);
                    actionScene = new ActionScene(this, spriteBatch);
                    if(MediaPlayer.IsMuted)
                    {
                        actionScene.myMute();
                    }    
                    Components.Add(actionScene);
                    
                    pauseScreen.hide();
                    startScene.show();
                }
            }

            oldstate = ks;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
