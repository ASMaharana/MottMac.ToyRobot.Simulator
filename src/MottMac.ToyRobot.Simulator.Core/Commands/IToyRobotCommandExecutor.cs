namespace MottMac.ToyRobot.Simulator.Core.Commands
{
    public interface IToyRobotCommandExecutor
    {
        string Execute(Data.ToyRobot robot);
    }
}