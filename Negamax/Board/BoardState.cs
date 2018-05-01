using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Negamax.Board
{
    class BoardState
    {
        private Piece[,] mPiecesOnBoard;
        private List<Piece> mWhiteCaptures = new List<Piece>();
        private List<Piece> mBlackCaptures = new List<Piece>();
        private List<Move> mValidMoves = new List<Move>();

        public ushort BoardSize { get; private set; }
        public IList<Piece> WhiteCaptures { get { return mWhiteCaptures.AsReadOnly(); } }
        public IList<Piece> BlackCaptures { get { return mBlackCaptures.AsReadOnly(); } }

        public BoardState()
        {
            BoardSize = StandardBoard.BOARD_DIM;
            mPiecesOnBoard = new Piece[StandardBoard.BOARD_DIM, StandardBoard.BOARD_DIM];
            StoreValidMoves();
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

            StoreValidMoves();
        }

        public bool AddPieceToBoard(Piece piece, ushort xPos, ushort yPos)
        {
            bool success = false;

            if (IsPosValid(xPos, yPos)) {
                if (mPiecesOnBoard[xPos, yPos] == null) {
                    mPiecesOnBoard[xPos, yPos] = piece;
                    success = true;
                }
            }

            return success;
        }

        public void CapturePieceAt(ushort xPos, ushort yPos)
        {
            if (IsPosValid(xPos, yPos)) {
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
            if (IsPosValid(xPos, yPos)) {
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
            if (IsPosValid(move.Start.X, move.Start.Y) &&
                IsPosValid(move.End.X, move.End.Y)) {

                // Capture the piece at the destination first:
                CapturePieceAt(move.End.X, move.End.Y);

                // Move the piece:
                mPiecesOnBoard[move.End.X, move.End.Y] = mPiecesOnBoard[move.Start.X, move.Start.Y];
                mPiecesOnBoard[move.Start.X, move.Start.Y] = null;
            }
        }

        private void StoreValidMoves()
        {
            mValidMoves.Clear();

            for (ushort x = 0; x < BoardSize; x++) {
                for (ushort y = 0; y < BoardSize; y++) {
                    if (mPiecesOnBoard[x, y] != null) {
                        switch (mPiecesOnBoard[x, y].PieceType) {
                            case PieceType.Bishop:
                                StoreValidMovesForBishop(mPiecesOnBoard[x, y], x, y);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void StoreValidMovesForBishop(Piece bishop, ushort xPos, ushort yPos)
        {
            if (IsPosValid(xPos, yPos) && (bishop.PieceType == PieceType.Bishop)) {
                ushort x = xPos;
                ushort y = yPos;
                while ((x++ < BoardSize) && (y++ < BoardSize)) {
                    if (mPiecesOnBoard[x, y] == null) {

                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsPosValid(ushort xPos, ushort yPos)
        {
            return (xPos < BoardSize) && (yPos < BoardSize);
        }
    }
}
