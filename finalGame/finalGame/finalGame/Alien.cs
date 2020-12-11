/* File Name: Alien.cs
 * Purpose: Contains logic for aliens in the game
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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace finalGame
{
    /// <summary>
    /// The Alien Object
    /// </summary>
    public class Alien : MyObject
    {
        float vertDistanceToMove;
        Bullet bullet;
        Texture2D bulletTex;
        List<Bullet> bulletList = new List<Bullet>();
        Game1 game;
        Random rand;
        int level;
        public List<Bullet> BulletList { get => bulletList; set => bulletList = value; }



        /// <summary>
        /// Instatiates a new alien object
        /// </summary>
        /// <param name="game">The game object</param>
        /// <param name="spriteBatch">The scene's spritebatch</param>
        /// <param name="tex">The 2d texture for the alien</param>
        /// <param name="position">The position </param>
        public Alien(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, bool moveLeft, int level, Random rand) : base(game)
        {
            this.game = (Game1)game;
            SpriteBatch = spriteBatch;
            Tex = tex;
            Position = position;
            vertDistanceToMove = tex.Height + 10;
            bulletTex = game.Content.Load<Texture2D>("Images/bulletRedSmall");
            this.rand = rand;
            int speedJump = 0;
            this.level = level;
            if (level > 1)
            {
                speedJump = 1;
            }
            int speed = 2 + (level / 4) + speedJump;

            if (moveLeft)
            {
                Speed = new Vector2(-speed, 0);
            }
            else
            {
                Speed = new Vector2(speed, 0);
            }
        }
        /// <summary>
        /// Draw the alien
        /// </summary>
        /// <param name="gameTime">gametime from game</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Tex, Position, Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Handles movement of the alien
        /// Handles the shooting of the alien
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Position += Speed;

            if (Position.X + Tex.Width > Shared.stage.X)
            {
                Position = new Vector2(Shared.stage.X - Tex.Width, Position.Y + vertDistanceToMove);
                Speed = -Speed;
            }
            else if (Position.X < 0)
            {
                Position = new Vector2(0, Position.Y + vertDistanceToMove);
                Speed = -Speed;
            }

            bool working = true;
            int faster = level * 30;
            int fastJump = 0;
            if (level > 2)
            {
                fastJump = 50;
            }
            int speed = 800 - faster - fastJump;
            if (level != 1)
            {
                if (rand.Next(0, speed) == 1)
                {
                    bullet = new Bullet(game, SpriteBatch, bulletTex, new Vector2(Position.X + Tex.Width / 2, Position.Y + bulletTex.Height), true);
                    game.Components.Add(bullet);
                    bulletList.Add(bullet);
                }
            }
            base.Update(gameTime);
        }
    }
}
