using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AllInOne
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //Scenes
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, 
                                       graphics.PreferredBackBufferHeight);
            startScene = new StartScene(this, spriteBatch);
            startScene.show();

            actionScene = new ActionScene(this, spriteBatch);
            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(startScene);
            this.Components.Add(actionScene);
            this.Components.Add(helpScene);
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
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if(selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    actionScene.show();
                }
                if(selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    helpScene.show();
                }
                if(selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if(actionScene.Enabled)
            {
                if(ks.IsKeyDown(Keys.Escape))
                {
                    startScene.show();
                    actionScene.hide();
                }
            }
            if (helpScene.Enabled)
            {
                if(ks.IsKeyDown(Keys.Escape))
                {
                    helpScene.hide();
                    startScene.show();
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
