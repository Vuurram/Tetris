using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

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


    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
    static Random random;

    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameState gameState;

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

        grid = new TetrisGrid();
        currentPosition = new Vector2(4, 0);
        currentTetrisBlock = new TetrisBlock(RandomBlockShape());
        nextTetrisBlock = new TetrisBlock(RandomBlockShape());


    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if(inputHelper.KeyPressed(Keys.Up))
        {   
            currentTetrisBlock.RotateBlocks();
            SetPositionCorrect();
        }

        if ((inputHelper.KeyDown(Keys.Down)) && (IsShiftPossible(0, 1)) && (timer <= 0))
        {
            timer = 3;
            currentPosition.Y++;
        }

        if ((inputHelper.KeyDown(Keys.Right)) && (IsShiftPossible(1, 0)) && (timer <= 0)) 
        {
            timer = 6;
            currentPosition.X++;          
        }

        if ((inputHelper.KeyDown(Keys.Left)) && (IsShiftPossible(-1, 0)) && (timer <= 0))
        {
            timer = 6;
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

        if (currentPosition.X + tetrisBlockHeight > grid.Width)
        {
            currentPosition.X = grid.Width - tetrisBlockHeight;
        }
    }

    public bool IsShiftPossible(int newX, int newY)
    {
        for (int i = 0; i < currentTetrisBlock.blockShape.GetLength(0); i++) 
        {
            for (int j = 0; j < currentTetrisBlock.blockShape.GetLength(1); j++)
            {
                if (currentTetrisBlock.blockShape[i, j] == 1)
                {
                    int gridX = (int)currentPosition.X + newX + i;
                    int gridY = (int)currentPosition.Y + newY + j;

                    if ((gridX < 0) || (gridX >= grid.Width) || (gridY >= grid.Height) || (grid.grid[gridX, gridY] == 1))
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
                if (currentTetrisBlock.blockShape[i, j] == 1)
                {
                    int gridX = (int)currentPosition.X + i;
                    int gridY = (int)currentPosition.Y + j;

                
                    grid.grid[gridX, gridY] = 1;
                    grid.gridColors[gridX, gridY] = currenTetrisBlock.blockColor;
                
                    
                    //try
                    //{
                    //    grid.grid[gridX, gridY] = 1;
                    //}
                    //catch (Exception e)
                    //{
                    //    Debug.WriteLine($"{gridX} | {gridY}");
                    //}
                }
            }
        }
    }
    public void Update(GameTime gameTime)
    {
        delta += (float)gameTime.ElapsedGameTime.TotalSeconds;
        timer--;

        if (delta >= shiftSpeed)
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
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch, currentPosition, currentTetrisBlock);
        currentTetrisBlock.Draw(gameTime, spriteBatch, grid.GridPosition, currentPosition);
        nextTetrisBlock.Draw(gameTime, spriteBatch, Vector2.Zero, Vector2.Zero);
        //spriteBatch.DrawString(font, "Hello!", Vector2.Zero, Color.Blue);
        spriteBatch.End();
    }

    public void Reset()
    {
        currentPosition = new Vector2(4, 0);
        currentTetrisBlock = nextTetrisBlock;
        nextTetrisBlock = new TetrisBlock(RandomBlockShape());
    }
}
