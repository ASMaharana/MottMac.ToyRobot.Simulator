using System;
using System.Text.RegularExpressions;
using MottMac.ToyRobot.Simulator.Data;

namespace MottMac.ToyRobot.Simulator.Core.Commands
{
    public class ToyRobotPlaceCommand : ToyRobotCommandBase
    {
        public static string CommandName => "PLACE";
        private static readonly Regex PlaceCommandRegex = new Regex($"^((?i){CommandName} (?-i))(?<x>\\d),(?<y>\\d),(?<cardinalDirection>(?i){CardinalDirection.North}|{CardinalDirection.South}|{CardinalDirection.East}|{CardinalDirection.West}(?-i))", RegexOptions.Compiled);
        public override string Name => CommandName;
        public int X { get; set; }
        public int Y { get; set; }
        public CardinalDirection CardinalDirection { get; set; }

        public override bool Validate(string input)
        {
            var regexMatchResult = PlaceCommandRegex.Match(input);
            if (!regexMatchResult.Success)
            {
                return default;
            }

            X = int.Parse(regexMatchResult.Groups["x"].Value);
            Y = int.Parse(regexMatchResult.Groups["y"].Value);
            CardinalDirection = Enum.Parse<CardinalDirection>(regexMatchResult.Groups["cardinalDirection"].Value, true);
            return true;
        }

        public override string Execute(Data.ToyRobot robot)
        {
            robot.Place(new Coordinate(X, Y), CardinalDirection);
            return default;
        }
        
    }
}