using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CommandAPI.Tests;

public class CommandsControllerTests : IDisposable
{
    Mock<ICommandAPIRepo>? mockRepo;
    CommandsProfile? realProfile;
    MapperConfiguration? configuration;
    IMapper? mapper;

    public CommandsControllerTests()
    {
        mockRepo = new Mock<ICommandAPIRepo>();
        realProfile = new CommandsProfile();
        configuration = new MapperConfiguration(config => config.AddProfile(realProfile));
        mapper = new Mapper(configuration);
    }

    public void Dispose()
    {
        mockRepo = null;
        realProfile = null;
        configuration = null;
        mapper = null;
    }

    [Fact]
    public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
    {
        // Arrange
        mockRepo!.Setup(repo => 
        repo.GetAllCommands()).Returns(GetCommands(0));
        
        // use an actual instance of mapper for more benefits
        var controller = new CommandsController(mockRepo.Object, mapper!);

        // Act
        var result = controller.GetAllCommands();

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
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
    
    [Fact]
    public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
        var controller = new CommandsController(mockRepo.Object, mapper!);

        // Act
        var result = controller.GetAllCommands();

        // Assert
        var okResult = result.Result as OkObjectResult;
        var commands = okResult!.Value as List<CommandReadDto>;
        Assert.Single(commands);
    }

    [Fact]
    public void GetAllCommands_Returns200Ok_WhenDBHasOneResource()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

        var controller = new CommandsController(mockRepo.Object, mapper!);

        // Act
        var result = controller.GetAllCommands();

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

        var controller = new CommandsController(mockRepo.Object, mapper!);

        // Act
        var result = controller.GetAllCommands();

        // Assert
        Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
    }

    [Fact]
    public void GetCommandByID_Returns404NotFound_WhenNonExistentIDProvided()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(0)).Returns(() => null);

        var controller = new CommandsController(mockRepo.Object, mapper!);

        // Act
        var result = controller.GetCommandById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void GetCommandByID_Returns200Ok_WhenValidIDProvided()
    {
        // Arrange
        mockRepo!.Setup(repo => repo.GetCommandById(1)).Returns(new Command {
            Id = 1,
            HowTo = "mock",
            CommandLine = "mock",
            Platform = "mock"
        });

        var controller = new CommandsController(mockRepo.Object, mapper!);

        // Act
        var result = controller.GetCommandById(1);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<ActionResult<CommandReadDto>>(result);
        // very usefull
    }
}