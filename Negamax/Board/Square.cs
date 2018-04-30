using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Negamax.Board
{
    class Square : IDisposable
    {
        public Piece Piece { get; private set; }
        public bool IsSquareOccupied { get { return Piece != null; } }

        private Texture2D mSquareTexture;

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="squareTexture">The texture to render for the square.</param>
        public Square(Texture2D squareTexture)
        {
            mSquareTexture = squareTexture;
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="squareTexture">The texture to render for the square.</param>
        /// <param name="piece">The piece on this square.</param>
        public Square(Texture2D squareTexture, Piece piece)
        {
            mSquareTexture = squareTexture;
            AddPiece(piece);
        }

        /// <summary>
        /// Adds a piece to the square if unoccupied.
        /// </summary>
        /// <param name="piece">The new piece on the square.</param>
        /// <returns>True if piece was placed; false if square was occupied.</returns>
        public bool AddPiece(Piece piece)
        {
            if (IsSquareOccupied) {
                return false;
            }

            Piece = piece;
            return true;
        }

        /// <summary>
        /// Replaces the piece on this square.
        /// </summary>
        /// <param name="piece">The new piece for the square.</param>
        public void ReplacePiece(Piece piece)
        {
            Piece = piece;
        }

        /// <summary>
        /// Removes the piece from the square.
        /// </summary>
        public void RemovePiece()
        {
            Piece = null;
        }

        /// <summary>
        /// Renders the square to the screen.
        /// </summary>
        /// <remarks>
        /// Only call this method after calling spriteBatch.Begin()!
        /// </remarks>
        /// <param name="spriteBatch">The Began sprite batch.</param>
        /// <param name="destination">The rectangle to draw the square to.</param>
        public void DrawSquare(SpriteBatch spriteBatch, Rectangle destination)
        {
            if (spriteBatch != null) {
                spriteBatch.Draw(mSquareTexture, destination, Color.White);

                if (Piece != null) {
                    Piece.DrawPiece(spriteBatch, destination);
                }
            }
        }

        public void Dispose()
        {
            mSquareTexture = null;

            // Dispose the piece if it exists:
            if (Piece != null) {
                Piece.Dispose();
            }
        }
    }
}
