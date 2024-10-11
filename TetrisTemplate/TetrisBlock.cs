using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq.Expressions;
using Tetris;
namespace Tetris
{
    class TetrisBlock
    {
        public enum TetrisBlocks
        {
            L, J, O, I, S, Z, T
        }
        public int[,] blockShape { get; set; }
        public TetrisBlocks Blocks { get; private set; }
        Texture2D emptyCell;

        Color color;

        public TetrisBlock(TetrisBlocks blocks)
        {
            Blocks = blocks;
            blockShape = GetBlockShape(blocks);
            emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
            color = new Color(0, 0, 0);
        }

        private int[,] GetBlockShape(TetrisBlocks blocks)
        {
            switch (blocks)
            {
                case TetrisBlocks.L:
                    return new int[,]
                    {
                        {0 ,0 ,1 },
                        {1, 1, 1 },
                        {0, 0, 0 }

                    };
                case TetrisBlocks.J:
                    return new int[,]
                    {
                        {1 ,0 ,0},
                        {1, 1, 1},
                        {0, 0, 0}

                    };
                case TetrisBlocks.O:
                    return new int[,]
                    {
                        {1, 1},
                        {1, 1}
                    };
                case TetrisBlocks.I:
                    return new int[,]
                    {
                        {0 ,1, 0, 0},
                        {0, 1, 0, 0},
                        {0, 1, 0, 0},
                        {0, 1, 0, 0}

                    };
                case TetrisBlocks.S:
                    return new int[,]
                    {
                        {0, 1, 1},
                        {1, 1, 0},
                        {0, 0, 0 }
                    };
                case TetrisBlocks.Z:
                    return new int[,]
                    {
                        {1 ,1 ,0},
                        {0, 1, 1 },
                        {0, 0, 0 }

                    };
                case TetrisBlocks.T:
                    return new int[,]
                    {
                        {0, 0, 0},
                        {1, 1, 1},
                        {0, 1, 0}
                    };
                default:
                    return new int[4, 4];
            }
        }

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

        public void Draw(GameTime gametime, SpriteBatch spriteBatch, Vector2 gridPosition, Vector2 currentPosition)
        {
            for (int i = 0; i < blockShape.GetLength(0); i++)
            {
                for (int j = 0; j < blockShape.GetLength(1); j++)
                {
                    if (blockShape[i, j] == 1)
                    {                       
                        spriteBatch.Draw(emptyCell ,new Vector2(gridPosition.X + (currentPosition.X + i) * emptyCell.Width, gridPosition.Y + (currentPosition.Y + j) * emptyCell.Height), Color.Red);
                    }
                }
            }


        }
    }
}
    


