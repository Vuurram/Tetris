using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris;
using Microsoft.Xna.Framework.Input;

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
    private Vector2 currentPosition;

    private float shiftSpeed = 1.0f;
    private float delta;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

        grid = new TetrisGrid();
        currentPosition = new Vector2(4, 0);
        currentTetrisBlock = new TetrisBlock(RandomBlockShape());

        
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if(inputHelper.KeyPressed(Keys.Up) == true)
        {
            
                currentTetrisBlock.RotateBlocks();
            
        }

        if (inputHelper.KeyPressed(Keys.Down) == true)
        {

            currentPosition.Y -= 1;

        }

        if (inputHelper.KeyPressed(Keys.Right) == true) 
        {
            
                currentPosition.X += 1;
            
        }

        if (inputHelper.KeyPressed(Keys.Left) == true)
        {
           
                currentPosition.X -= 1;
            
        }
    }



    public void Update(GameTime gameTime)
    {
        delta += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (delta >= shiftSpeed)
        {
            delta = 0.0f;
            currentPosition.Y += 1;

        }
    }

  

    public TetrisBlock.TetrisBlocks RandomBlockShape()
    {
        return (TetrisBlock.TetrisBlocks)random.Next(0, 7);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);
        currentTetrisBlock.Draw(gameTime, spriteBatch, grid.GridPosition, currentPosition);
        //spriteBatch.DrawString(font, "Hello!", Vector2.Zero, Color.Blue);
        spriteBatch.End();
    }

    public void Reset()
    {
        currentPosition = new Vector2(4, 0);
        currentTetrisBlock = new TetrisBlock(RandomBlockShape());
    }

}
