namespace MottMac.ToyRobot.Simulator.Core.Commands
{
    public interface IToyRobotCommand : IToyRobotCommandExecutor, IToyRobotCommandValidator
    {
        string Name { get; }
    }
}