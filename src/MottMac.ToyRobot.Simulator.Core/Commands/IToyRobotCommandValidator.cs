namespace MottMac.ToyRobot.Simulator.Core.Commands
{
    public interface IToyRobotCommandValidator
    {
        bool Validate(string command);
    }

}