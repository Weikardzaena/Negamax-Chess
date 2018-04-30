using System;

namespace Negamax.Board
{
    public struct Move
    {
        public UInt16 StartX { get; private set; }
        public UInt16 StartY { get; private set; }
        public UInt16 EndX { get; private set; }
        public UInt16 EndY { get; private set; }
        public PieceColor Color { get; private set; }
        public bool IsCapture { get; private set; }
        public bool IsCheck { get; private set; }
        public bool IsMate { get; private set; }

        public Move(UInt16 startX,
                    UInt16 startY,
                    UInt16 endX,
                    UInt16 endY,
                    PieceColor color,
                    bool isCapture,
                    bool isCheck,
                    bool isMate)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
            Color = color;
            IsCapture = isCapture;
            IsCheck = isCheck;
            IsMate = isMate;
        }
    }
}
