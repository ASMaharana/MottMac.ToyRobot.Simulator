namespace MottMac.ToyRobot.Simulator.Data
{
    public readonly struct Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public int X { get; }
        public int Y { get; }

        public static Coordinate None = new Coordinate(-1, -1);
    }
}