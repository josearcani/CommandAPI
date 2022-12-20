using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Moq;

namespace CommandAPI.Tests;

public class CommandsControllerTests
{
    [Fact]
    public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
    {
        // Arrange
        var mockRepo = new Mock<ICommandAPIRepo>();
        mockRepo.Setup(repo => 
        repo.GetAllCommands()).Returns(GetCommands(0));

        var realProfile = new CommandsProfile();
        var configuration = new MapperConfiguration(cfg => 
            cfg.AddProfile(realProfile));
        IMapper mapper = new Mapper(configuration);
        // use an actual instance of mapper for more benefits
        var controller = new CommandsController(mockRepo.Object, mapper);

        // Act

        // Assert
    }

    private List<Command> GetCommands(int num)
    {
        var commands = new List<Command>();
        if (num > 0)
        {
            commands.Add(new Command
            {
                Id = 0,
                HowTo = "How to generate a migration",
                CommandLine = "dotnet ef migrations add <Name of Migration>",
                Platform = ".Net Core EF"
            });
        }
        return commands;
    }
}