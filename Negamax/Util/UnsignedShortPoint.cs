namespace Negamax.Util
{
    public struct UnsignedShortPoint
    {
        public ushort X { get; private set; }
        public ushort Y { get; private set; }

        public UnsignedShortPoint(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public UnsignedShortPoint(ushort value)
        {
            X = value;
            Y = value;
        }
    }
}
