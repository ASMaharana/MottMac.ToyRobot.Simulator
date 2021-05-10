using System;
using MottMac.ToyRobot.Simulator.Data;

namespace MottMac.ToyRobot.Simulator.Core.Commands
{
    public class ToyRobotMoveCommand : ToyRobotCommandBase
    {
        public override string Name => "MOVE";
        
        public override string Execute(Data.ToyRobot robot)
        {
            if (!robot.IsActive)
            {
                return default;
            }

            var x = robot.Coordinate.X;
            var y = robot.Coordinate.Y;
            var cardinalDirectionInt = (int)robot.CardinalDirection;

            var isXAxis = cardinalDirectionInt % 2 == 0;
            if (isXAxis)
            {
                if (cardinalDirectionInt == 0)
                {
                    x -= 1;
                }
                else
                {
                    x += 1;
                }
            }
            else
            {
                if (cardinalDirectionInt == 1)
                {
                    y += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            robot.Place(new Coordinate(x, y), robot.CardinalDirection);
            return default;
        }
    }
}