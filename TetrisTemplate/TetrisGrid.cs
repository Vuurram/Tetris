using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using Tetris;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    public Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 gridPosition;

    public int[,] grid; 

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }
   
    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    public Vector2 GridPosition { get { return gridPosition; } }
    TetrisBlock TetrisBlock;
    int score = 0;
    SpriteFont font;

    
    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        gridPosition = new Vector2((1920- (emptyCell.Width * Width)) /2, (1080 - (emptyCell.Height * Height)) / 2);
        grid = new int[Width, Height];
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        Clear();
    }



    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    //public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 currentPosition, TetrisBlock currentBlock)
    //{
    //    for (int i = 0; i < grid.GetLength(0); i++)
    //    {
    //        for (int j = 0; j < grid.GetLength(1); j++)
    //        {
    //            if (grid[i, j] == 1)
    //            {
    //                spriteBatch.Draw(emptyCell, new Vector2(gridPosition.X + i * emptyCell.Width, gridPosition.Y + j * emptyCell.Height), gridColors[i, j]);
    //            }
    //            else
    //            {
    //                spriteBatch.Draw(emptyCell, new Vector2(gridPosition.X + i * emptyCell.Width, gridPosition.Y + j * emptyCell.Height), Color.White);
    //            }
    //        }
    //    }
    //}

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 currentPosition, TetrisBlock currentBlock)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                FullRow();
                Color color = GetColor(grid[i, j]);
                spriteBatch.Draw(emptyCell, new Vector2(gridPosition.X + i * emptyCell.Width, gridPosition.Y + j * emptyCell.Height), color);
            }
        }
        int textScale = 2;
        string points = "The score is: " + score.ToString();
        Vector2 textSize = font.MeasureString(points);
        Vector2 textPosition = new Vector2((1920 - textSize.X * textScale) / 2, (900 - textSize.Y * textScale));
        spriteBatch.DrawString(font, points, textPosition, Color.Black, 0, Vector2.Zero, textScale, SpriteEffects.None, 0);
    }

    public Color GetColor(int gridValue)
    {
        switch (gridValue)
        {
            case 1: return Color.Cyan;
            case 2: return Color.Red;
            case 3: return Color.Blue;
            case 4: return Color.Green;
            case 5: return Color.Purple;
            case 6: return Color.Orange;
            case 7: return Color.Black;
            default: return Color.White;
        }
    }

    void FullRow()
    {
        int rowsFull = 0;
        for (int y = grid.GetLength(1) - 1; y > 0; y--)
        {
            int row = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (grid[x, y] != 0)
                {
                    row ++;
                } else
                {
                    row = 0;
                }
            }
            if (row == grid.GetLength(0))
            {
                RowDropper(y);
                rowsFull++;
            }
        }
        Score(rowsFull);
    }

    void RowDropper(int y)
    {
        int min = y;
        for (int i = 0; i < min; i++)
        {
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                grid[j, y] = grid[j, y - 1];
            }
            y -= 1;
        }  
    }

    void Score(int rowsFull)
    {
        score += Math.Max(0, rowsFull / 2) * 2000 + rowsFull * 1000;
    }

    public void Clear()
    {
        grid = new int[Width, Height];
    }
}

