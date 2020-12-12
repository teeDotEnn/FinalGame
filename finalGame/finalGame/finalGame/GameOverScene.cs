/* File: GameOverScene.cs
 * Purpose: To render a menu
 * Rev History:
 *          Created 2020-12-09
 *          Stephen Draper

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

namespace finalGame
{
    /// <summary>
    /// Render a gameOver scene
    /// </summary>
    public class GameOverScene : GameScene
    {
        private SpriteFont font;
        private SpriteFont fontHead;
        private Vector2 headPosition = new Vector2(25, 50);
        private Vector2 textPosition = new Vector2(25, 100);
        private int score;
        private Game1 game;
        private bool enterName = false;
        private int newScoreLocation;
        private int lineIncriment;
        private string[] initials = {"_","_","_"};
        Keys[] oldPressedKeys = Keyboard.GetState().GetPressedKeys();
        private int initialIndex = 0;
        private bool finished = false;
        private bool returnToMain = false;
        /// <summary>
        /// Bool to see if the user wants to move back to the main menu
        /// </summary>
        public bool ReturnToMain { get => returnToMain; set => returnToMain = value; }
        List<int> highscoreList = new List<int>();
        List<string> nameList = new List<string>();
        /// <summary>
        /// Creates a new game over scene
        /// </summary>
        /// <param name="game">game</param>
        /// <param name="spriteBatch">spritebatch</param>
        /// <param name="score">the user's score</param>
        public GameOverScene(Game game, SpriteBatch spriteBatch, int score) : base(game)
        {
            this.game = (Game1)game;
            SpriteBatch = spriteBatch;
            font = game.Content.Load<SpriteFont>("Fonts/HelpFont");
            fontHead = game.Content.Load<SpriteFont>("Fonts/HelpFontBold");
            this.score = score;
        }
        /// <summary>
        /// Draw the various options possible
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            lineIncriment = fontHead.LineSpacing + 10;
            SpriteBatch.Begin();
            SpriteBatch.DrawString(fontHead, "Bro you died.", headPosition, Color.White);
            SpriteBatch.DrawString(fontHead, $"Score: {score}", textPosition, Color.White);
            if(enterName == true)
            {
                SpriteBatch.DrawString(fontHead, $"You got a new High Score!", new Vector2(textPosition.X, textPosition.Y+lineIncriment), Color.White);
                SpriteBatch.DrawString(fontHead, $"Enter your initials:", new Vector2(textPosition.X, textPosition.Y + lineIncriment*2), Color.White);
                SpriteBatch.DrawString(fontHead, $"{initials[0]} {initials[1]} {initials[2]}", new Vector2(textPosition.X, textPosition.Y + lineIncriment * 3), Color.White);
            }
            if(finished == true)
            {
                SpriteBatch.DrawString(fontHead, $"You entered: {initials[0]} {initials[1]} {initials[2]}", new Vector2(textPosition.X, textPosition.Y + lineIncriment * 6), Color.White);
                SpriteBatch.DrawString(fontHead, $"Is this correct? Enter to confirm,", new Vector2(textPosition.X, textPosition.Y + lineIncriment * 7), Color.White);
                SpriteBatch.DrawString(fontHead, $"Esc to redo.", new Vector2(textPosition.X, textPosition.Y + lineIncriment * 8), Color.White);

            }
            if(enterName == false)
            {
                SpriteBatch.DrawString(font, "Press Esc to menu.", new Vector2(Shared.stage.X - 300, Shared.stage.Y - 50), Color.White);
            }
            SpriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Checks to see if the user is typing, loads in the old file and updates the high score file
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if(File.Exists(game.Filepath))
            {
                nameList.Clear();
                highscoreList.Clear();
                string[] linesFromFile = File.ReadAllLines(game.Filepath);
                foreach (string line in linesFromFile)
                {
                    string[] fields = line.Split('|');
                    nameList.Add(fields[0]);
                    highscoreList.Add(int.Parse(fields[1]));
                }
            }

            for (int i = 0; i < highscoreList.Count; i++)
            {
                if(score > highscoreList[i])
                {
                    newScoreLocation = i;
                    enterName = true;
                    break;
                }
            }

            if(enterName == true)
            {
                KeyboardState ks = Keyboard.GetState();
                Keys[] pressedKeys = ks.GetPressedKeys();
                
                foreach (Keys key in pressedKeys)
                {
                    char checkLetter = (char)key;
                    if(!oldPressedKeys.Contains(key) && char.IsLetterOrDigit(checkLetter))
                    {
                        OnKeyDown(key);
                    }
                }

                oldPressedKeys = pressedKeys;
            }
            if(finished == true)
            {
                KeyboardState ks = Keyboard.GetState();
                if(ks.IsKeyDown(Keys.Enter))
                {
                    nameList.Insert(newScoreLocation, (string.Join("", initials) + "....."));
                    highscoreList.Insert(newScoreLocation, score);
                    nameList.RemoveAt(nameList.Count - 1);
                    highscoreList.RemoveAt(highscoreList.Count - 1);

                    File.WriteAllText(game.Filepath, string.Empty);
                    using (StreamWriter writer = new StreamWriter(game.Filepath, true))
                    {
                        int highscoreIndex = 0;
                        foreach (string name in nameList)
                        {
                            string stringToWrite = $"{name}|{highscoreList[highscoreIndex]}";
                            writer.WriteLine(stringToWrite);
                            highscoreIndex++;
                        }
                    }
                    returnToMain = true;
                }
                else if(ks.IsKeyDown(Keys.Escape))
                {
                    finished = false;
                    initialIndex = 0;
                    initials[0] = "_";
                    initials[1] = "_";
                    initials[2] = "_";
                }
            }

            if(enterName == false)
            {
                if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    returnToMain = true;
                }
            }

            base.Update(gameTime);
        }

        private void OnKeyDown(Keys key)
        {
            if(initialIndex < 3)
            {
                initials[initialIndex] = key.ToString();
                initialIndex++;
            }
            if(initialIndex == 3)
            {
                finished = true;
            }
        }
    }
}