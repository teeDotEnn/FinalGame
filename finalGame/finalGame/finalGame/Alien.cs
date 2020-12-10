﻿/* File Name: Alien.cs
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
        /// <summary>
        /// Instatiates a new alien object
        /// </summary>
        /// <param name="game">The game object</param>
        /// <param name="spriteBatch">The scene's spritebatch</param>
        /// <param name="tex">The 2d texture for the alien</param>
        /// <param name="position">The position </param>
        public Alien(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, bool moveLeft) : base(game)
        {
            SpriteBatch = spriteBatch;
            Tex = tex;
            Position = position;
            vertDistanceToMove = tex.Height + 10;
            // TO DO 
            if (moveLeft)
            {
                Speed = new Vector2(-20, 0);
            }
            else
            {
                Speed = new Vector2(20, 0);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Tex, Position, Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed;

            if(Position.X+Tex.Width>Shared.stage.X)
            {
                Position = new Vector2(Shared.stage.X-Tex.Width, Position.Y + vertDistanceToMove);
                Speed = -Speed;
            }
            else if (Position.X < 0)
            {
                Position = new Vector2(0, Position.Y + vertDistanceToMove);
                Speed = -Speed;
            }
            base.Update(gameTime);
        }
    }
}
