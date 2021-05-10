namespace MottMac.ToyRobot.Simulator.Data
{
    public class ToyRobot
    {
        private readonly Matrix _matrix;
        public ToyRobot(Matrix matrix)
        {
            _matrix = matrix;
        }

        public bool IsActive => CardinalDirection != CardinalDirection.None && !Coordinate.Equals(Coordinate.None);
        public Coordinate Coordinate { get; private set; } = Coordinate.None;
        public CardinalDirection CardinalDirection { get; private set; } = CardinalDirection.None;
        public void Place(Coordinate coordinate, CardinalDirection cardinalDirection)
        {
            if (coordinate.X >= _matrix.Columns || coordinate.Y >= _matrix.Rows || coordinate.X < 0 || coordinate.Y < 0)
            {
                return;
            }
            
            Coordinate = coordinate;
            CardinalDirection = cardinalDirection;
        }
    }
}