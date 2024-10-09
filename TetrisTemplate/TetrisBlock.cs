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
            L, J, O, I, N, Z, T
        }
        public int[,] ShapeBlock { get; set; }
        public TetrisBlocks Blocks { get; private set; }
        public Vector2 position;
        Texture2D emptyCell;


        public TetrisBlock(TetrisBlocks blocks)
        {
            Blocks = blocks;
            ShapeBlock = GetBlockShape(blocks);
            position = Vector2.Zero;
            emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");

        }

        private int[,] GetBlockShape(TetrisBlocks blocks)
        {
            switch (blocks)
            {
                case TetrisBlocks.L:
                    return new int[,]
                    {
                        {1 ,0 ,0 },
                        {1, 0, 0 },
                        {1, 1, 0 }

                    };
                case TetrisBlocks.J:
                    return new int[,]
                    {
                        {0 ,0 ,1},
                        {0, 0, 1},
                        {0, 1, 1}

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
                        {1 ,1, 1, 1},
                        {0, 0, 0, 0},
                        {0, 0, 0, 0},
                        {0, 0, 0, 0}

                    };
                case TetrisBlocks.N:
                    return new int[,]
                    {
                        {0, 1, 0},
                        {1, 1, 0},
                        {1, 0, 0 }
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


        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ShapeBlock.GetLength(0); i++)
            {
                for (int j = 0; j < ShapeBlock.GetLength(1); j++)
                {
                    if (ShapeBlock[i, j] == 1)
                    {
                        spriteBatch.Draw(emptyCell, new Vector2(position.X + i * emptyCell.Width, position.Y + j * emptyCell.Height), Color.Red);
                    }
                }
            }


        }
    }
}
    


