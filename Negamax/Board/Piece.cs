using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Negamax.Board
{
    public enum PieceColor
    {
        Black,
        White,
    }

    public enum PieceType
    {
        Bishop,
        King,
        Knight,
        Pawn,
        Queen,
        Rook,
    }

    public class Piece : IDisposable
    {
        public readonly PieceColor PieceColor;
        public readonly PieceType PieceType;

        private Texture2D mPieceTexture;

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="pieceTexture">The texture for this piece.</param>
        /// <param name="color">Which in-game color the piece is.</param>
        /// <param name="type">The type of piece this is.</param>
        public Piece(Texture2D pieceTexture, PieceColor color, PieceType type)
        {
            mPieceTexture = pieceTexture;
            PieceColor = color;
            PieceType = type;
        }

        /// <summary>
        /// Renders the piece to the screen.
        /// </summary>
        /// <remarks>
        /// Only call this method after calling spriteBatch.Begin()!
        /// </remarks>
        /// <param name="spriteBatch">The Began sprite batch.</param>
        /// <param name="destination">The rectangle to draw the piece to.</param>
        public void DrawPiece(SpriteBatch spriteBatch, Rectangle destination)
        {
            if (spriteBatch != null) {
                spriteBatch.Draw(mPieceTexture, destination, Color.White);
            }
        }

        public void Dispose()
        {
            mPieceTexture = null;
        }
    }
}
