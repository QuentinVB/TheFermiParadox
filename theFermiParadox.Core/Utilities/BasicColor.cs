namespace theFermiParadox.Core
{
    public struct BasicColor
    {
        public BasicColor(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public override string ToString()
        {
            return $"R:{R},G:{G},B:{B}";

        }
    }
}