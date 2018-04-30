using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Negamax.Board
{
    class Square : IDisposable
    {
        private Texture2D mSquareTexture;

        public ushort X { get; private set; }
        public ushort Y { get; private set; }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="squareTexture">The texture to render for the square.</param>
        public Square(Texture2D squareTexture, ushort x, ushort y)
        {
            X = x;
            Y = y;
            mSquareTexture = squareTexture;
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
            }
        }

        public void Dispose()
        {
            mSquareTexture = null;
        }
    }
}
