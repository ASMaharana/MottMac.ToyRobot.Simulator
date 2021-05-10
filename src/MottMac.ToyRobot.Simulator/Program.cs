using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MottMac.ToyRobot.Simulator.Core;
using MottMac.ToyRobot.Simulator.Core.Commands;
using System;
using System.Collections.Generic;
using System.IO;

namespace MottMac.ToyRobot.Simulator
{
    internal class Program
    {
        private static IServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            RegisterServices();

            if (args.Length != 2)
            {
                Console.WriteLine("Please provide the input and output files");
            }

            using (var inputFile = new StreamReader(File.OpenRead(args[0])))
            {
                using var outputFile = new StreamWriter(File.Create(args[1]));
                var commandProcessorFacade = _serviceProvider.GetService<IToyRobotCommandProcessorFacade>();

                var executeResult = commandProcessorFacade?.Execute(5, 5, ReadCommands(inputFile));
                if (!string.IsNullOrEmpty(executeResult))
                {
                    outputFile.WriteLine(executeResult);
                }
            }
            DisposeServices();
        }

        public static IEnumerable<string> ReadCommands(TextReader inputReader)
        {
            using (inputReader)
            {
                string line;
                while ((line = inputReader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            var builder = new ContainerBuilder();

            builder.RegisterType<ToyRobotCommandProcessorFacade>().As<IToyRobotCommandProcessorFacade>();
            builder.RegisterType<ToyRobotCommandValidatorFacade>().As<IToyRobotCommandValidatorFacade>();
            builder.RegisterAssemblyTypes(typeof(IToyRobotCommand).Assembly)
                .Where(x => x.Name.EndsWith("Command"))
                .AsImplementedInterfaces();

            builder.Populate(collection);

            var appContainer = builder.Build();

            _serviceProvider = new AutofacServiceProvider(appContainer);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
