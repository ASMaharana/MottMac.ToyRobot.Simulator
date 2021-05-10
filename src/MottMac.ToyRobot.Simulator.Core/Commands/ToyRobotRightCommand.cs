using MottMac.ToyRobot.Simulator.Data;

namespace MottMac.ToyRobot.Simulator.Core.Commands
{
    public class ToyRobotRightCommand : ToyRobotCommandBase
    {
        public override string Name => "RIGHT";
        public override string Execute(Data.ToyRobot robot)
        {
            if (!robot.IsActive)
            {
                return default;
            }
            
            var cardinalDirectionInt = (int)robot.CardinalDirection;
            cardinalDirectionInt += 1;
            cardinalDirectionInt %= 4;

            robot.Place(robot.Coordinate, (CardinalDirection)cardinalDirectionInt);
            return default;
        }
    }
}