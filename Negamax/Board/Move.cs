using System;

namespace Negamax.Board
{
    public struct Move
    {
        public UInt16 StartX { get; private set; }
        public UInt16 StartY { get; private set; }
        public UInt16 EndX { get; private set; }
        public UInt16 EndY { get; private set; }

        public Move(UInt16 startX,
                    UInt16 startY,
                    UInt16 endX,
                    UInt16 endY)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
        }
    }
}
