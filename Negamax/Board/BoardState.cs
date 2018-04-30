using System.Collections.Generic;

namespace Negamax.Board
{
    class BoardState
    {
        private Piece[,] mPiecesOnBoard;
        private List<Piece> mWhiteCaptures = new List<Piece>();
        private List<Piece> mBlackCaptures = new List<Piece>();

        private ushort mBoardSize;

        public BoardState()
        {
            mBoardSize = StandardBoard.BOARD_DIM;
            mPiecesOnBoard = new Piece[StandardBoard.BOARD_DIM, StandardBoard.BOARD_DIM];
        }

        public bool AddPieceToBoard(Piece piece, ushort xPos, ushort yPos)
        {
            bool success = false;

            if ((xPos < mBoardSize) && (yPos < mBoardSize)) {
                if (mPiecesOnBoard[xPos, yPos] == null) {
                    mPiecesOnBoard[xPos, yPos] = piece;
                    success = true;
                }
            }

            return success;
        }

        public void AddPieceToWhiteCaptures(Piece piece)
        {
            mWhiteCaptures.Add(piece);
        }

        public void AddPieceToBlackCaptures(Piece piece)
        {
            mBlackCaptures.Add(piece);
        }

        public Piece PieceAt(ushort xPos, ushort yPos)
        {
            if ((xPos < mBoardSize) && (yPos < mBoardSize)) {
                return mPiecesOnBoard[xPos, yPos];
            }
            return null;
        }
    }
}
