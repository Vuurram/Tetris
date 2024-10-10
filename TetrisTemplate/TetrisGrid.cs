using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq.Expressions;
using Tetris;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    int[,] grid; 

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }
   
    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }
    TetrisBlock TetrisBlock;

    
    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        gridPosition = new Vector2((1920- (emptyCell.Width * Width)) /2, (1080 - (emptyCell.Height * Height)) / 2);
        grid = new int[Width, Height];
        Clear();
    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 gridPosition, Vector2 currentPosition)
    {
        grid = new int[10, 20];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Vector2 drawPosition = new Vector2(
                    gridPosition.X + (currentPosition.X + i) * emptyCell.Width,  // i is de kolom
                    gridPosition.Y + (currentPosition.Y + j) * emptyCell.Height  // j is de rij
                );

                spriteBatch.Draw(emptyCell, drawPosition, Color.Red);

            }
        }
    }

        /// <summary>
        /// Clears the grid.
        /// </summary>
        public void Clear()
    {
    }
}

