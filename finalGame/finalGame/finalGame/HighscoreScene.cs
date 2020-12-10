using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace finalGame
{
    public class HighscoreScene : GameScene
    {
        private Game1 game;
        private SpriteFont font;
        private SpriteFont fontHead;
        private Vector2 headPosition = new Vector2(25, 50);
        private Vector2 bodyPosition = new Vector2(25, 120);
        private int lineDisplacement;
        private List<string> nameList;
        private List<int> highscoreList;
        private int highscore;
        public int Highscore { get => highscore; set => highscore = value; }

        public HighscoreScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.game = (Game1)game;
            nameList = new List<string>();
            highscoreList = new List<int>();
            

            SpriteBatch = spriteBatch;
            font = game.Content.Load<SpriteFont>("Fonts/HelpFont");
            fontHead = game.Content.Load<SpriteFont>("Fonts/HelpFontBold");
        }

        

        public override void Draw(GameTime gameTime)
        {
            lineDisplacement = font.LineSpacing + 10;
            int lineNo = 0;
            int highscoreIndex = 0;
            SpriteBatch.Begin();
            SpriteBatch.DrawString(fontHead, "High Scores", headPosition, Color.White);
            foreach (string name in nameList)
            {
                string stringToPrint = $"{highscoreIndex+1}: {name}{highscoreList[highscoreIndex]}";
                SpriteBatch.DrawString(font, stringToPrint, new Vector2(bodyPosition.X, bodyPosition.Y + (lineDisplacement * lineNo)), Color.White);
                lineNo++;
                highscoreIndex++; // I realize these two numbers are the same, but we weren't sure if we might change them up, so we left them split
            }
            SpriteBatch.DrawString(font, "Press Esc to return.", new Vector2(Shared.stage.X - 300, Shared.stage.Y - 50), Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            
            

            nameList.Clear();
            highscoreList.Clear();

            if (File.Exists(game.Filepath))
            {
                string[] linesFromFile = File.ReadAllLines(game.Filepath);
                foreach (string line in linesFromFile)
                {
                    string[] fields = line.Split('|');
                    nameList.Add(fields[0]);
                    highscoreList.Add(int.Parse(fields[1]));
                }
            }

            if(highscoreList.Count>0)
            {
                highscore = highscoreList[0];
            }
            base.Update(gameTime);
        }
    }
}
