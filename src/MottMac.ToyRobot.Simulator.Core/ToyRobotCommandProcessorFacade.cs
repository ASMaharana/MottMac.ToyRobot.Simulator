using System.Collections.Generic;
using MottMac.ToyRobot.Simulator.Data;

namespace MottMac.ToyRobot.Simulator.Core
{
    public interface IToyRobotCommandProcessorFacade
    {
        string Execute(int row, int col, IEnumerable<string> commands);
    }

    public class ToyRobotCommandProcessorFacade : IToyRobotCommandProcessorFacade
    {
        private readonly IToyRobotCommandValidatorFacade _commandValidatorFactory;
        public ToyRobotCommandProcessorFacade(IToyRobotCommandValidatorFacade commandValidatorFactory)
        {
            _commandValidatorFactory = commandValidatorFactory;
        }

        public string Execute(int row, int col, IEnumerable<string> commands)
        {
            var toyRobot = new Data.ToyRobot(new Matrix(col, row));
            string output = default;
            foreach (var command in commands)
            {
                var validationResult = _commandValidatorFactory.Execute(command);
                var executeResult = validationResult?.Execute(toyRobot);

                if (executeResult == default) continue;
                output = executeResult;
            }

            return output;
        }
    }
}