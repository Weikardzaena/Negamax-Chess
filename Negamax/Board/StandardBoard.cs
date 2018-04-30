using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Negamax.Board
{
    class StandardBoard : IDisposable
    {
        public const UInt16 SQUARE_DIM = 80;
        public const UInt16 BOARD_DIM = 8;

        private const string PIECES_PATH = @".\Assets\Pieces\";
        private const string BOARD_PATH = @".\Assets\Board\";

        private BoardState mCurrentState = new BoardState();
        private BoardState mPreviousState;

        private Square[,] mSquares = new Square[BOARD_DIM, BOARD_DIM];
        private Rectangle[,] mSquareLocations = new Rectangle[BOARD_DIM, BOARD_DIM];

        private Square mSelectedSquare;

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
        private Texture2D T_SquareSelected;

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
            T_SquareSelected = contentManager.Load<Texture2D>(BOARD_PATH + "Square_Selected");

            // Create board squares:
            for (UInt16 x = 0; x < 8; x++) {
                for (UInt16 y = 0; y < 8; y++) {
                    // Initialize the square with the appropriate color:
                    if ((y % 2) == 0) {
                        if ((x % 2) == 0) {
                            mSquares[x, y] = new Square(T_SquareDark, x, y);
                        } else {
                            mSquares[x, y] = new Square(T_SquareLight, x, y);
                        }
                    } else {
                        // Note the reverse order of the square color:
                        if ((x % 2) == 0) {
                            mSquares[x, y] = new Square(T_SquareLight, x, y);
                        } else {
                            mSquares[x, y] = new Square(T_SquareDark, x, y);
                        }
                    }

                    // Initialize the positions of the squares:
                    mSquareLocations[x, y] = new Rectangle(new Point(x * SQUARE_DIM, (BOARD_DIM - 1 - y) * SQUARE_DIM), new Point(SQUARE_DIM));
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
            if (spriteBatch != null) {
                for (UInt16 x = 0; x < BOARD_DIM; x++) {
                    for (UInt16 y = 0; y < BOARD_DIM; y++) {
                        mSquares[x, y].DrawSquare(spriteBatch, mSquareLocations[x, y]);
                        if (mCurrentState.PieceAt(x, y) != null)
                            mCurrentState.PieceAt(x, y).DrawPiece(spriteBatch, mSquareLocations[x, y]);
                    }
                }

                // Draw the visual indicator over the selected square:
                if (mSelectedSquare != null) {
                    spriteBatch.Draw(T_SquareSelected, mSquareLocations[mSelectedSquare.X, mSelectedSquare.Y], Color.White);
                }
            }
        }

        public void HandleClick(Point clickLocation)
        {
            /* This section checks for which square the user clicked on. */

            bool foundX = false;
            bool foundY = false;
            UInt16 yIndex = 0;
            UInt16 xIndex = 0;

            for (xIndex = 0; xIndex < BOARD_DIM; xIndex++) {
                if ((mSquareLocations[xIndex, 0].Right > clickLocation.X) &&
                    (mSquareLocations[xIndex, 0].Left <= clickLocation.X)) {
                    foundX = true;
                    break;
                }
            }
            if (foundX) {
                for (yIndex = 0; yIndex < BOARD_DIM; yIndex++) {
                    if ((mSquareLocations[xIndex, yIndex].Top <= clickLocation.Y) &&
                        (mSquareLocations[xIndex, yIndex].Bottom > clickLocation.Y)) {
                        foundY = true;
                        break;
                    }
                }
            }

            if (foundX && foundY) {
                if (mSelectedSquare == null) {
                    if (mCurrentState.PieceAt(xIndex, yIndex) != null) {
                        mSelectedSquare = mSquares[xIndex, yIndex];
                    }
                } else {
                    if ((mSelectedSquare.X != xIndex) || (mSelectedSquare.Y != yIndex)) {

                        // TODO:  Check if this square is a valid move.
                        // TODO:  Apply move
                    }

                    // Don't forget to deselect the square!
                    ClearCurrentSelection();
                }
            } else {
                ClearCurrentSelection();
            }
        }

        public void ClearCurrentSelection()
        {
            mSelectedSquare = null;
        }

        public void Dispose()
        {
            // Dispose all Squares:
            foreach (var square in mSquares) {
                if (square != null) {
                    square.Dispose();
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
            mCurrentState.AddPieceToBoard(new Piece(T_RookWhite, PieceColor.White, PieceType.Rook), 0, 0);
            mCurrentState.AddPieceToBoard(new Piece(T_KnightWhite, PieceColor.White, PieceType.Knight), 1, 0);
            mCurrentState.AddPieceToBoard(new Piece(T_BishopWhite, PieceColor.White, PieceType.Bishop), 2, 0);
            mCurrentState.AddPieceToBoard(new Piece(T_QueenWhite, PieceColor.White, PieceType.Queen), 3, 0);
            mCurrentState.AddPieceToBoard(new Piece(T_KingWhite, PieceColor.White, PieceType.King), 4, 0);
            mCurrentState.AddPieceToBoard(new Piece(T_BishopWhite, PieceColor.White, PieceType.Bishop), 5, 0);
            mCurrentState.AddPieceToBoard(new Piece(T_KnightWhite, PieceColor.White, PieceType.Knight), 6, 0);
            mCurrentState.AddPieceToBoard(new Piece(T_RookWhite, PieceColor.White, PieceType.Rook), 7, 0);

            mCurrentState.AddPieceToBoard(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn), 0, 1);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn), 1, 1);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn), 2, 1);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn), 3, 1);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn), 4, 1);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn), 5, 1);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn), 6, 1);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnWhite, PieceColor.White, PieceType.Pawn), 7, 1);

            mCurrentState.AddPieceToBoard(new Piece(T_RookBlack, PieceColor.Black, PieceType.Rook), 0, 7);
            mCurrentState.AddPieceToBoard(new Piece(T_KnightBlack, PieceColor.Black, PieceType.Knight), 1, 7);
            mCurrentState.AddPieceToBoard(new Piece(T_BishopBlack, PieceColor.Black, PieceType.Bishop), 2, 7);
            mCurrentState.AddPieceToBoard(new Piece(T_QueenBlack, PieceColor.Black, PieceType.Queen), 3, 7);
            mCurrentState.AddPieceToBoard(new Piece(T_KingBlack, PieceColor.Black, PieceType.King), 4, 7);
            mCurrentState.AddPieceToBoard(new Piece(T_BishopBlack, PieceColor.Black, PieceType.Bishop), 5, 7);
            mCurrentState.AddPieceToBoard(new Piece(T_KnightBlack, PieceColor.Black, PieceType.Knight), 6, 7);
            mCurrentState.AddPieceToBoard(new Piece(T_RookBlack, PieceColor.White, PieceType.Rook), 7, 7);

            mCurrentState.AddPieceToBoard(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn), 0, 6);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn), 1, 6);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn), 2, 6);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn), 3, 6);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn), 4, 6);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn), 5, 6);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn), 6, 6);
            mCurrentState.AddPieceToBoard(new Piece(T_PawnBlack, PieceColor.Black, PieceType.Pawn), 7, 6);
        }
    }
}
