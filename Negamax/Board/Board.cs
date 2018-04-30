using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Negamax.Board
{
    class StandardBoard : IDisposable
    {
        public const UInt16 SQUARE_DIM = 60;
        public const UInt16 BOARD_DIM = 8;

        private const string PIECES_PATH = @".\Assets\Pieces\";
        private const string BOARD_PATH = @".\Assets\Board\";

        private List<List<Square>> mSquares = new List<List<Square>>();
        private List<List<Rectangle>> mSquareLocations = new List<List<Rectangle>>();

        private Texture2D T_BishopWhite;
        private Texture2D T_BishopBlack;
        private Texture2D T_KingWhite;
        private Texture2D T_KingBlack;
        private Texture2D T_KnightWhite;
        private Texture2D T_KnightBlack;
        private Texture2D T_PawnWhite;
        private Texture2D T_PawnBlack;
        private Texture2D T_QueenWhite;
        private Texture2D T_QueenBlack;
        private Texture2D T_RookWhite;
        private Texture2D T_RookBlack;

        private Texture2D T_SquareDark;
        private Texture2D T_SquareLight;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="contentManager">The content manager where the assets are loaded.</param>
        public StandardBoard(ContentManager contentManager)
        {
            // Load textures:
            T_BishopWhite = contentManager.Load<Texture2D>(PIECES_PATH + "Bishop_White");
            T_BishopBlack = contentManager.Load<Texture2D>(PIECES_PATH + "Bishop_Black");
            T_KingWhite = contentManager.Load<Texture2D>(PIECES_PATH + "King_White");
            T_KingBlack = contentManager.Load<Texture2D>(PIECES_PATH + "King_Black");
            T_KnightWhite = contentManager.Load<Texture2D>(PIECES_PATH + "Knight_White");
            T_KnightBlack = contentManager.Load<Texture2D>(PIECES_PATH + "Knight_Black");
            T_PawnWhite = contentManager.Load<Texture2D>(PIECES_PATH + "Pawn_White");
            T_PawnBlack = contentManager.Load<Texture2D>(PIECES_PATH + "Pawn_Black");
            T_QueenWhite = contentManager.Load<Texture2D>(PIECES_PATH + "Queen_White");
            T_QueenBlack = contentManager.Load<Texture2D>(PIECES_PATH + "Queen_Black");
            T_RookWhite = contentManager.Load<Texture2D>(PIECES_PATH + "Rook_White");
            T_RookBlack = contentManager.Load<Texture2D>(PIECES_PATH + "Rook_Black");

            T_SquareDark = contentManager.Load<Texture2D>(BOARD_PATH + "Square_Dark");
            T_SquareLight = contentManager.Load<Texture2D>(BOARD_PATH + "Square_Light");

            // Create board squares:
            for (UInt16 x = 0; x < 8; x++) {

                // Initialize rows entries first:
                mSquares.Add(new List<Square>(BOARD_DIM));
                mSquareLocations.Add(new List<Rectangle>(BOARD_DIM));

                for (UInt16 y = 0; y < 8; y++) {
                    // Initialize the square with the appropriate color:
                    if ((y % 2) == 0) {
                        if ((x % 2) == 0) {
                            mSquares[x].Add(new Square(T_SquareDark));
                        } else {
                            mSquares[x].Add(new Square(T_SquareLight));
                        }
                    } else {
                        // Note the reverse order of the square color:
                        if ((x % 2) == 0) {
                            mSquares[x].Add(new Square(T_SquareLight));
                        } else {
                            mSquares[x].Add(new Square(T_SquareDark));
                        }
                    }

                    // Initialize the positions of the squares:
                    mSquareLocations[x].Add(new Rectangle(new Point(x * SQUARE_DIM, (BOARD_DIM - 1 - y) * SQUARE_DIM), new Point(SQUARE_DIM)));
                }
            }

            // Set up the board to initial state:
            SetupBoard();
        }

        /// <summary>
        /// Renders the board to the screen.
        /// </summary>
        /// <remarks>
        /// Only call this method after calling spriteBatch.Begin()!
        /// </remarks>
        /// <param name="spriteBatch">The Began sprite batch.</param>
        public void DrawBoard(SpriteBatch spriteBatch)
        {
            for (UInt16 x = 0; x < BOARD_DIM; x++) {
                for (UInt16 y = 0; y < BOARD_DIM; y++) {
                    mSquares[x][y].DrawSquare(spriteBatch, mSquareLocations[x][y]);
                }
            }
        }

        public void HandleClick(Point clickLocation)
        {
            int yIndex = -1;
            int xIndex = -1;

            for (UInt16 x = 0; x < BOARD_DIM; x++) {
                if ((mSquareLocations[x][0].Right > clickLocation.X) &&
                    (mSquareLocations[x][0].Left <= clickLocation.X)) {
                    xIndex = x;
                    break;
                }
            }
            if ((xIndex >= 0) && (xIndex < BOARD_DIM)) {
                for (UInt16 y = 0; y < BOARD_DIM; y++) {
                    if ((mSquareLocations[xIndex][y].Top <= clickLocation.Y) &&
                        (mSquareLocations[xIndex][y].Bottom > clickLocation.Y)) {
                        yIndex = y;
                        break;
                    }
                }
            }
            Console.WriteLine("Got index {0}, {1} for click", xIndex, yIndex);
        }

        public void Dispose()
        {
            // Dispose all Squares:
            foreach (var row in mSquares) {
                foreach (var square in row) {
                    if (square != null) {
                        square.Dispose();
                    }
                }
            }

            // Remove references to all assets:
            T_BishopWhite = null;
            T_BishopBlack = null;
            T_KingWhite = null;
            T_KingBlack = null;
            T_KnightWhite = null;
            T_KnightBlack = null;
            T_PawnWhite = null;
            T_PawnBlack = null;
            T_QueenWhite = null;
            T_QueenBlack = null;
            T_RookWhite = null;
            T_RookBlack = null;

            T_SquareDark = null;
            T_SquareLight = null;
        }

        /// <summary>
        /// Sets all the pieces to their beginning states.
        /// </summary>
        private void SetupBoard()
        {
            mSquares[0][0].AddPiece(new Piece(T_RookWhite, PieceColor.White, PieceType.Rook));
            mSquares[1][0].AddPiece(new Piece(T_KnightWhite, PieceColor.White, PieceType.Knight));
            mSquares[2][0].AddPiece(new Piece(T_BishopWhite, PieceColor.White, PieceType.Bishop));
            mSquares[3][0].AddPiece(new Piece(T_QueenWhite, PieceColor.White, PieceType.Queen));
            mSquares[4][0].AddPiece(new Piece(T_KingWhite, PieceColor.White, PieceType.King));
            mSquares[5][0].AddPiece(new Piece(T_BishopWhite, PieceColor.White, PieceType.Bishop));
            mSquares[6][0].AddPiece(new Piece(T_KnightWhite, PieceColor.White, PieceType.Knight));
            mSquares[7][0].AddPiece(new Piece(T_RookWhite, PieceColor.White, PieceType.Rook));

            mSquares[0][1].AddPiece(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn));
            mSquares[1][1].AddPiece(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn));
            mSquares[2][1].AddPiece(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn));
            mSquares[3][1].AddPiece(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn));
            mSquares[4][1].AddPiece(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn));
            mSquares[5][1].AddPiece(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn));
            mSquares[6][1].AddPiece(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn));
            mSquares[7][1].AddPiece(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn));

            mSquares[0][7].AddPiece(new Piece(T_RookBlack, PieceColor.Black, PieceType.Rook));
            mSquares[1][7].AddPiece(new Piece(T_KnightBlack, PieceColor.Black, PieceType.Knight));
            mSquares[2][7].AddPiece(new Piece(T_BishopBlack, PieceColor.Black, PieceType.Bishop));
            mSquares[3][7].AddPiece(new Piece(T_QueenBlack, PieceColor.Black, PieceType.Queen));
            mSquares[4][7].AddPiece(new Piece(T_KingBlack, PieceColor.Black, PieceType.King));
            mSquares[5][7].AddPiece(new Piece(T_BishopBlack, PieceColor.Black, PieceType.Bishop));
            mSquares[6][7].AddPiece(new Piece(T_KnightBlack, PieceColor.Black, PieceType.Knight));
            mSquares[7][7].AddPiece(new Piece(T_RookBlack, PieceColor.White, PieceType.Rook));

            mSquares[0][6].AddPiece(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn));
            mSquares[1][6].AddPiece(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn));
            mSquares[2][6].AddPiece(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn));
            mSquares[3][6].AddPiece(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn));
            mSquares[4][6].AddPiece(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn));
            mSquares[5][6].AddPiece(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn));
            mSquares[6][6].AddPiece(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn));
            mSquares[7][6].AddPiece(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn));
        }
    }
}
