using MottMac.ToyRobot.Simulator.Data;

namespace MottMac.ToyRobot.Simulator.Core.Commands
{
    public class ToyRobotLeftCommand : ToyRobotCommandBase
    {
        public override string Name => "LEFT";

        public override string Execute(Data.ToyRobot robot)
        {
            if (!robot.IsActive)
            {
                return default;
            }
            
            var cardinalDirectionInt = (int) robot.CardinalDirection;
            cardinalDirectionInt -= 1;
            if (cardinalDirectionInt < 0)
            {
                cardinalDirectionInt = 3;
            }
            robot.Place(robot.Coordinate, (CardinalDirection)cardinalDirectionInt);
            return default;
        }
    }
}