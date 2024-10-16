using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

/// <summary>
/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
/// </summary>
class GameWorld
{
    /// <summary>
    /// An enum for the different game states that the game can have.
    /// </summary>
    enum GameState
    {
        Playing,
        GameOver
    }


    int level = 1;

    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
    static Random random;
    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font;
    Texture2D background;
    /// <summary>
    /// The current game state.
    /// </summary>
    GameState gameState = GameState.Playing;

   // SoundEffect placingSoundEffect;
    /// <summary>
    /// The main grid of the game.
    /// </summary>
    TetrisGrid grid;

    TetrisBlock currentTetrisBlock;
    TetrisBlock nextTetrisBlock;
    private Vector2 currentPosition;

    private float shiftSpeed = 1.0f;
    private float delta;
    public float timer;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        background = TetrisGame.ContentManager.Load<Texture2D>("Background/TetrisBackground3");

        grid = new TetrisGrid();
        currentPosition = new Vector2(4, 0);
        currentTetrisBlock = new TetrisBlock(RandomBlockShape());
        nextTetrisBlock = new TetrisBlock(RandomBlockShape());
       // placingSoundEffect = TetrisGame.ContentManager.Load<SoundEffect>("PlacingSoundEffect");


    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if(inputHelper.KeyPressed(Keys.Up))
        {   
            currentTetrisBlock.RotateBlocks();
            SetPositionCorrect();
        }

        if (inputHelper.KeyPressed(Keys.Space))
        {
            while (IsShiftPossible(0, 1))
                currentPosition.Y++;
        }

        if ((inputHelper.KeyDown(Keys.Down)) && (IsShiftPossible(0, 1)) && (timer <= 0))
        {
            timer = 3;
            currentPosition.Y++;
        }

        if ((inputHelper.KeyDown(Keys.Right)) && (IsShiftPossible(1, 0)) && (timer <= 0)) 
        {
            timer = 8;
            currentPosition.X++;          
        }

        if ((inputHelper.KeyDown(Keys.Left)) && (IsShiftPossible(-1, 0)) && (timer <= 0))
        {
            timer = 8;
            currentPosition.X--;           
        }
       
    }

    public void SetPositionCorrect()
    {
        int tetrisBlockWidth = currentTetrisBlock.blockShape.GetLength(0);
        int tetrisBlockHeight = currentTetrisBlock.blockShape.GetLength(1);

        if (currentPosition.X < 0)
        { 
            currentPosition.X = 0;
        }

        if (currentPosition.Y < 0)
        {
            currentPosition.Y = 0;
        }

        if (currentPosition.X + tetrisBlockWidth > grid.Width)
        {
            currentPosition.X = grid.Width - tetrisBlockWidth;
        }

        if (currentPosition.Y + tetrisBlockHeight > grid.Height)
        {
            currentPosition.Y = grid.Height - tetrisBlockHeight;
        }

        

    }

    public bool IsShiftPossible(int newX, int newY)
    {
        for (int i = 0; i < currentTetrisBlock.blockShape.GetLength(0); i++) 
        {
            for (int j = 0; j < currentTetrisBlock.blockShape.GetLength(1); j++)
            {
                if (currentTetrisBlock.blockShape[i, j] != 0)
                {
                    int gridX = (int)currentPosition.X + newX + i;
                    int gridY = (int)currentPosition.Y + newY + j;

                    if ((gridX < 0) || (gridX >= grid.Width) || (gridY >= grid.Height) || (grid.grid[gridX, gridY] != 0))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void LockBlock(TetrisBlock currenTetrisBlock, Vector2 currentPosition)
    {
        for (int i = 0; i < currentTetrisBlock.blockShape.GetLength(0); i++)
        {
            for (int j = 0; j < currentTetrisBlock.blockShape.GetLength(1); j++)
            {
                if (currentTetrisBlock.blockShape[i, j] != 0)
                {
                    int gridX = (int)currentPosition.X + i;
                    int gridY = (int)currentPosition.Y + j;

                    grid.grid[gridX, gridY] = currentTetrisBlock.blockShape[i, j];
                   // placingSoundEffect.Play();

                } 
            }
        }
    }
    public void Update(GameTime gameTime)
    {
        timer--;
        delta += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (grid.score >= 4000 * level) level++;
        if (delta >= shiftSpeed - Math.Pow(level, 0.6f) * 0.1f)
        {
            delta = 0.0f;

            if (!IsShiftPossible(0, 1))
            {
                LockBlock(currentTetrisBlock, currentPosition);
                Reset();
            }
            currentPosition.Y++; 
        }
    }
  

    public TetrisBlock.TetrisBlocks RandomBlockShape()
    {
        return (TetrisBlock.TetrisBlocks)random.Next(0, 7);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (gameState == GameState.GameOver) return;
        spriteBatch.Begin();
        spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
        grid.Draw(gameTime, spriteBatch, currentPosition, currentTetrisBlock);
        currentTetrisBlock.Draw(gameTime, spriteBatch, grid.GridPosition, currentPosition);
        nextTetrisBlock.Draw(gameTime, spriteBatch, Vector2.Zero, Vector2.Zero);

        int textScale = 2;
        string points = "The Level is: " + level.ToString();
        Vector2 textSize = font.MeasureString(points);
        Vector2 textPosition = new Vector2((300 - textSize.X * textScale) / 2, (300 - textSize.Y * textScale));
        spriteBatch.DrawString(font, points, textPosition, Color.White, 0, Vector2.Zero, textScale, SpriteEffects.None, 0);
        spriteBatch.End();
    }

    public void Reset()
    {
        currentPosition = new Vector2(4, 0);
        currentTetrisBlock = nextTetrisBlock;
        nextTetrisBlock = new TetrisBlock(RandomBlockShape());
        for (int i = 0; i < currentTetrisBlock.blockShape.GetLength(0); i++)
        {
            for (int j = 0; j < currentTetrisBlock.blockShape.GetLength(1); j++)
            {
                if (currentTetrisBlock.blockShape[i, j] != 0)
                {
                    int gridX = (int)currentPosition.X + i;
                    int gridY = (int)currentPosition.Y + j;

                    if (grid.grid[gridX, gridY] != 0)
                    {
                        gameState = GameState.GameOver;
                        return;
                    }
                }
            }
        }
    }
}
