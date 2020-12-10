using System;
using System.Collections.Generic;
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
        private Alien alien;
        private List<Alien> alienList = new List<Alien>();
        private Texture2D alienTex;
        private Bullet bullet;
        private List<Bullet> ourBulletsList = new List<Bullet>();
        private Texture2D bulletTex;
        private Game game;
        private CollisionManager collisionManager;
        public CollisionManager CollisionManager { get => collisionManager; set => collisionManager = value; }

        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;
            this.game = game;
            


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
            alienList = generateAliens(1);
            foreach(Alien alien in alienList)
            {
                this.Components.Add(alien);
            }
            //This needs to go here
            collisionManager = new CollisionManager(game, spriteBatch, ourBulletsList, alienList);
            this.Components.Add(collisionManager);

            bulletTex = game.Content.Load<Texture2D>("Images/bullet");
        }

        private List<Alien> generateAliens(int level)
        {
            List<Alien> aliens = new List<Alien>();
            Vector2 location = new Vector2(Shared.stage.X / 4, Shared.stage.Y / 4);
            for (int i = 0; i < 12; i++)
            {
                if(i == 3 || i == 11)
                {
                    aliens.Add(new Alien(game, SpriteBatch, alienTex, location,true));
                }
                else
                {
                    aliens.Add(new Alien(game, SpriteBatch, alienTex, location, false));
                }
                
                //Need a better way to increment rows
                if(i == 3 || i == 7 || i == 11)
                {
                    location.X = (Shared.stage.X / 4);
                    location.Y = (location.Y + alienTex.Height + 10);
                }
                else
                {
                    location.X = location.X + alienTex.Width + 10;
                }
                
                
            }
            return aliens;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            alienList = collisionManager.AlienList;
            ourBulletsList = collisionManager.BulletList;

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Space))
            {
                bullet = new Bullet(game, SpriteBatch, bulletTex, new Vector2(ship.Position.X + ship.Tex.Width/2, ship.Position.Y-bulletTex.Height));
                this.Components.Add(bullet);
                ourBulletsList.Add(bullet);
            }

            collisionManager.AlienList = alienList;
            collisionManager.BulletList = ourBulletsList;

            int count = Components.Count;
            for (int i = 0; i < count; i++)
            {
                if(Components[i].Enabled == false)
                {
                    Components.RemoveAt(i);
                    count--;
                    i--;
                }
            }

            base.Update(gameTime);
        }

        public void myMute()
        {
            collisionManager.myMute();
        }
    }
}
