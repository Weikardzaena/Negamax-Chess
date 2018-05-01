using System;
using Negamax.Util;

namespace Negamax.Board
{
    public struct Move
    {
        public UnsignedShortPoint Start { get; private set; }
        public UnsignedShortPoint End { get; private set; }

        public Move(ushort startX,
                    ushort startY,
                    ushort endX,
                    ushort endY)
        {
            Start = new UnsignedShortPoint(startX, startY);
            End = new UnsignedShortPoint(endX, endY);
        }
    }
}
