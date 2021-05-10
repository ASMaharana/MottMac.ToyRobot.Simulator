namespace MottMac.ToyRobot.Simulator.Core.Commands
{
    public class ToyRobotReportCommand : ToyRobotCommandBase
    {
        public override string Name => "REPORT";

        public override string Execute(Data.ToyRobot robot) =>
            robot.IsActive ? $"{robot.Coordinate.X},{robot.Coordinate.Y},{robot.CardinalDirection}".ToUpper() : default;
    }
}