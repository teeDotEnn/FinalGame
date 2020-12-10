﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace finalGame
{
    public class CreditsScene : GameScene
    {
        private SpriteFont font;
        private SpriteFont fontHead;
        private Vector2 headPosition = new Vector2(25, 50);
        private Vector2 bodyPosition = new Vector2(25, 120);
        private int lineDisplacement;


        public CreditsScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;
            font = game.Content.Load<SpriteFont>("Fonts/HelpFont");
            fontHead = game.Content.Load<SpriteFont>("Fonts/HelpFontBold");
        }

        public override void Draw(GameTime gameTime)
        {
            lineDisplacement = font.LineSpacing + 10;
            SpriteBatch.Begin();
            SpriteBatch.DrawString(fontHead, "Credits", headPosition, Color.White);
            SpriteBatch.DrawString(font, "Creators:      Stephen Draper & Tim Nigh", bodyPosition, Color.White);
            SpriteBatch.DrawString(font, "Music:           Insert Here", new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement)), Color.White);
            SpriteBatch.DrawString(font, "Sprites:         See Word Doc!", new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement * 2)), Color.White);
            SpriteBatch.DrawString(font, "Press Esc to return.", new Vector2(Shared.stage.X - 300, Shared.stage.Y - 50), Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
