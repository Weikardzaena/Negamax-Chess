using System.Collections.Generic;

namespace Negamax.Board
{
    class BoardState
    {
        private Piece[,] mPiecesOnBoard;
        private List<Piece> mWhiteCaptures = new List<Piece>();
        private List<Piece> mBlackCaptures = new List<Piece>();

        public ushort BoardSize { get; private set; }
        public IList<Piece> WhiteCaptures { get { return mWhiteCaptures.AsReadOnly(); } }
        public IList<Piece> BlackCaptures { get { return mBlackCaptures.AsReadOnly(); } }

        public BoardState()
        {
            BoardSize = StandardBoard.BOARD_DIM;
            mPiecesOnBoard = new Piece[StandardBoard.BOARD_DIM, StandardBoard.BOARD_DIM];
        }

        public BoardState(BoardState other)
        {
            BoardSize = other.BoardSize;

            mPiecesOnBoard = new Piece[StandardBoard.BOARD_DIM, StandardBoard.BOARD_DIM];
            for (ushort x = 0; x < BoardSize; x++) {
                for (ushort y = 0; y < BoardSize; y++) {
                    // Copy the pieces on the board:
                    mPiecesOnBoard[x, y] = other.PieceAt(x, y);
                }
            }

            mWhiteCaptures.AddRange(other.WhiteCaptures);
            mBlackCaptures.AddRange(other.BlackCaptures);
        }

        public bool AddPieceToBoard(Piece piece, ushort xPos, ushort yPos)
        {
            bool success = false;

            if ((xPos < BoardSize) && (yPos < BoardSize)) {
                if (mPiecesOnBoard[xPos, yPos] == null) {
                    mPiecesOnBoard[xPos, yPos] = piece;
                    success = true;
                }
            }

            return success;
        }

        public void CapturePieceAt(ushort xPos, ushort yPos)
        {
            if ((xPos < BoardSize) && (yPos < BoardSize)) {
                if (mPiecesOnBoard[xPos, yPos] != null) {
                    if (mPiecesOnBoard[xPos, yPos].PieceColor == PieceColor.White) {
                        mBlackCaptures.Add(mPiecesOnBoard[xPos, yPos] );
                    } else if (mPiecesOnBoard[xPos, yPos].PieceColor == PieceColor.Black) {
                        mWhiteCaptures.Add(mPiecesOnBoard[xPos, yPos]);
                    }

                    mPiecesOnBoard[xPos, yPos] = null;
                }
            }
        }

        public Piece PieceAt(ushort xPos, ushort yPos)
        {
            if ((xPos < BoardSize) && (yPos < BoardSize)) {
                return mPiecesOnBoard[xPos, yPos];
            }
            return null;
        }

        /// <summary>
        /// Apply the move to the board.
        /// </summary>
        /// <remarks>
        /// This assumes the move is valid!
        /// </remarks>
        /// <param name="move">The move to apply.</param>
        public void ApplyMove(Move move)
        {
            if ((move.StartX < BoardSize) &&
                (move.StartY < BoardSize) &&
                (move.EndX < BoardSize) &&
                (move.EndY < BoardSize)) {

                if (mPiecesOnBoard[move.StartX, move.StartY] != null) {
                    if (mPiecesOnBoard[move.EndX, move.EndY] != null) {
                        if (mPiecesOnBoard[move.EndX, move.EndY].PieceColor != mPiecesOnBoard[move.StartX, move.StartY].PieceColor) {
                            CapturePieceAt(move.EndX, move.EndY);
                            mPiecesOnBoard[move.EndX, move.EndY] = mPiecesOnBoard[move.StartX, move.StartY];
                            mPiecesOnBoard[move.StartX, move.StartY] = null;
                        }
                    } else {
                        mPiecesOnBoard[move.EndX, move.EndY] = mPiecesOnBoard[move.StartX, move.StartY];
                        mPiecesOnBoard[move.StartX, move.StartY] = null;
                    }
                }
            }
        }
    }
}
