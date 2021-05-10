using System;

namespace MottMac.ToyRobot.Simulator.Core.Commands
{
    public abstract class ToyRobotCommandBase : IToyRobotCommand
    {
        public abstract string Execute(Data.ToyRobot robot);

        public virtual bool Validate(string command) => string.Equals(command, Name, StringComparison.OrdinalIgnoreCase);

        public abstract string Name { get; }
    }
}