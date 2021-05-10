using System.Collections.Generic;
using System.Linq;
using MottMac.ToyRobot.Simulator.Core.Commands;

namespace MottMac.ToyRobot.Simulator.Core
{
    public interface IToyRobotCommandValidatorFacade
    {
        IToyRobotCommand Execute(string commandLine);
    }
    public class ToyRobotCommandValidatorFacade : IToyRobotCommandValidatorFacade
    {
        private readonly IEnumerable<IToyRobotCommand> _commandValidators;
        public ToyRobotCommandValidatorFacade(IEnumerable<IToyRobotCommand> commandValidators)
        {
            _commandValidators = commandValidators;
        }

        public IToyRobotCommand Execute(string commandLine)
        {
            return _commandValidators
                .FirstOrDefault(validator => validator.Validate(commandLine));
        }
    }
}