namespace Day24
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Position { get; set; }
        public int Value { get; set; }
        public Point Clone()
        {
            return new Point
            {
                X = X,
                Y = Y,
                Value = Value
            };
        }
    }
}
