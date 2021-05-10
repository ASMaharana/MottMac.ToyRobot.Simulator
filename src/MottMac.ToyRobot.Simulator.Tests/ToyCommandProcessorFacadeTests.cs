using Autofac;
using MottMac.ToyRobot.Simulator.Core;
using MottMac.ToyRobot.Simulator.Core.Commands;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MottMac.ToyRobot.Simulator.Tests
{
    public class ToyCommandProcessorFacadeTests
    {
        [Fact]
        public void Execute_Commands_Success()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(5, 5, new List<string>
            {
                "PLACE 1,2,EAST",
                "MOVE",
                "MOVE",
                "LEFT",
                "MOVE",
                "REPORT",
            }.AsEnumerable());
            Assert.Equal("3,3,NORTH", outcome);
        }

        [Fact]
        public void Execute_Commands_OneMove_Success()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(5, 5, new List<string>
            {
                "PLACE 0,0,NORTH",
                "MOVE",
                "REPORT",
            }.AsEnumerable());
            Assert.Equal("0,1,NORTH", outcome);
        }

        [Fact]
        public void Execute_Commands_OneLeft_Success()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(5, 5, new List<string>
            {
                "PLACE 0,0,NORTH",
                "LEFT",
                "REPORT",
            }.AsEnumerable());
            Assert.Equal("0,0,WEST", outcome);
        }

        [Fact]
        public void Execute_Commands_NoReport_Failure()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(5, 5, new List<string>
            {
                "PLACE 0,0,NORTH",
                "LEFT",
            }.AsEnumerable());
            Assert.Equal(default, outcome);
        }

        [Fact]
        public void Execute_Commands_NoPlace_Failure()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(5, 5, new List<string>
            {
                "LEFT",
                "MOVE",
                "MOVE",
                "LEFT",
                "MOVE",
                "REPORT",

            }.AsEnumerable());
            Assert.Equal(default, outcome);
        }

        [Fact]
        public void Execute_Commands_InvalidPlace_Failure()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(5, 5, new List<string>
            {
                "PLACE 8,8,NORTH",
                "MOVE",
                "REPORT",

            }.AsEnumerable());
            Assert.Equal(default, outcome);
        }

        [Fact]
        public void Execute_Commands_InvalidPlace_NoDirection_Failure()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(5, 5, new List<string>
            {
                "PLACE 8,8",
                "MOVE",
                "REPORT"
            }.AsEnumerable());
            Assert.Equal(default, outcome);
        }

        [Fact]
        public void Execute_Commands_NoCommand_Failure()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(5, 5, new List<string>().AsEnumerable());
            Assert.Equal(default, outcome);
        }

        [Fact]
        public void Execute_Commands_MultiReport_Success()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(5, 5, new List<string>
            {
                "PLACE 1,2,EAST",
                "MOVE",
                "MOVE",
                "LEFT",
                "MOVE",
                "REPORT",
                "MOVE",
                "REPORT"
            }.AsEnumerable());
            Assert.Equal("3,4,NORTH", outcome);
        }

        [Fact]
        public void Execute_Commands_TryMovingOutSideRange_Success()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(3, 3, new List<string>
            {
                "PLACE 2,2,EAST",
                "MOVE",
                "MOVE",
                "REPORT"
            }.AsEnumerable());
            Assert.Equal("2,2,EAST", outcome);
        }

        [Fact]
        public void Execute_Commands_MultiLeft_Success()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(3, 3, new List<string>
            {
                "PLACE 2,2,EAST",
                "LEFT",
                "LEFT",
                "LEFT",
                "LEFT",
                "LEFT",
                "REPORT"
            }.AsEnumerable());
            Assert.Equal("2,2,NORTH", outcome);
        }

        [Fact]
        public void Execute_Commands_RightPlace_After_InvalidPlace_Success()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(3, 3, new List<string>
            {
                "PLACE 4,4,EAST",
                "MOVE",
                "LEFT",
                "RIGHT",
                "LEFT",
                "PLACE 1,1,EAST",
                "LEFT",
                "REPORT"
            }.AsEnumerable());
            Assert.Equal("1,1,NORTH", outcome);
        }

        [Fact]
        public void Execute_Commands_CaseInsensitive_With_MultiPlace_Success()
        {
            var container = ContainerBuilder();
            var cb = container.Build();
            var sut = cb.Resolve<ToyRobotCommandProcessorFacade>();
            var outcome = sut.Execute(3, 3, new List<string>
            {
                "place 1,1,north",
                "move",
                "left",
                "right",
                "place 1,1,south",
                "place 1,1,west",
                "place 1,1,east",
                "report"
            }.AsEnumerable());
            Assert.Equal("1,1,EAST", outcome);
        }

        private static ContainerBuilder ContainerBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ToyRobotCommandProcessorFacade>().As<IToyRobotCommandProcessorFacade>();
            builder.RegisterType<ToyRobotCommandValidatorFacade>().As<IToyRobotCommandValidatorFacade>();
            builder.RegisterAssemblyTypes(typeof(IToyRobotCommand).Assembly)
                .Where(x => x.Name.EndsWith("Command"))
                .AsImplementedInterfaces();
            builder.RegisterType<ToyRobotCommandProcessorFacade>();

            return builder;
        }
    }
}
