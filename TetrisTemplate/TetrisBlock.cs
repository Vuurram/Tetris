using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Linq.Expressions;
using Tetris;
namespace Tetris
{
    // Class that represents the TetrisBlocks
    class TetrisBlock
    {
        // Enum that contains the different blockshapes with the name most look a like
        public enum TetrisBlocks
        {
            L, J, O, I, S, Z, T
        }
        public int[,] blockShape { get; set; }

        public TetrisBlocks Blocks { get; private set; }
        Texture2D emptyCell;
        public int ColorID;
        TetrisGrid tetrisGrid;
        public TetrisBlock(TetrisBlocks blocks)
        {
            blockShape = GetBlockShape(blocks);
            block = TetrisGame.ContentManager.Load<Texture2D>("TetrisBlock");

            tetrisGrid = new TetrisGrid();
        }

        // Method with switch that gives the blockShape array a value and so a blockshape
        private int[,] GetBlockShape(TetrisBlocks blocks)
        {
            switch (blocks)
            {
                case TetrisBlocks.L: return new int[,] { { 0, 0, 1 }, { 1, 1, 1 }, { 0, 0, 0 } };
                case TetrisBlocks.J: return new int[,] { { 2, 0, 0 }, { 2, 2, 2 }, { 0, 0, 0 } };
                case TetrisBlocks.O: return new int[,] { { 3, 3 }, { 3, 3 } };
                case TetrisBlocks.I: return new int[,] { { 0, 4, 0, 0 }, { 0, 4, 0, 0 }, { 0, 4, 0, 0 }, { 0, 4, 0, 0 } };
                case TetrisBlocks.S: return new int[,] { { 0, 5, 5 }, { 5, 5, 0 }, { 0, 0, 0 } };
                case TetrisBlocks.Z: return new int[,] { { 6, 6, 0 }, { 0, 6, 6 }, { 0, 0, 0 } };
                case TetrisBlocks.T: return new int[,] { { 0, 0, 0 }, { 7, 7, 7 }, { 0, 7, 0 } };
                default: return new int[4, 4];
            }
        }

        // Method used to rotate the blocks
        // Is called when up key is pressed
        public void RotateBlocks()
        {
            int q = blockShape.GetLength(0);
            int r = blockShape.GetLength(1);
            int[,] rotatedBlockShape = new int[q, r];
            for (int x = 0; x < q; x++)
            {
                for (int y = 0; y < r; y++)
                {
                    rotatedBlockShape[y, q - 1 - x] = blockShape[x, y];
                }
            }
            blockShape = rotatedBlockShape;
        }

        // Reverse rotate the blocks if the rotate is not possible due to rotating in the position of a different block
        public void ReverseRotateBlocks()
        {
            int q = blockShape.GetLength(0);
            int r = blockShape.GetLength(1);
            int[,] rotatedBlockShape = new int[q, r];
            for (int x = 0; x < q; x++)
            {
                for (int y = 0; y < r; y++)
                {
                    rotatedBlockShape[r - 1 - y, x] = blockShape[x, y];
                }
            }
            blockShape = rotatedBlockShape;
        }

        // Method that draws the various blocks
        public void Draw(GameTime gametime, SpriteBatch spriteBatch, Vector2 gridPosition, Vector2 currentPosition)
        {
            for (int i = 0; i < blockShape.GetLength(0); i++)
            {
                for (int j = 0; j < blockShape.GetLength(1); j++)
                {
                    if (blockShape[i, j] != 0)
                    {
                        Color color = tetrisGrid.GetColor(blockShape[i, j]);
                        spriteBatch.Draw(block, new Vector2(gridPosition.X + (currentPosition.X + i) * block.Width, gridPosition.Y + (currentPosition.Y + j) * block.Height), color);
                    }
                }
            }
        }
    }
}