using CommandAPI.Models;

namespace CommandAPI.Tests;

public class CommandTests : IDisposable
{
    Command? testCommand;

    public CommandTests()
    {
        testCommand = new Command
        {
            HowTo = "Do Something",
            Platform = "Some Platform",
            CommandLine = "Some CommandLine"
        };
    }

    public void Dispose()
    {
        testCommand = null;
    }

    [Fact]
    public void CanChangeHowTo()
    {
        // <method name>_<expected result>_<contidion>
        // Arrange
        
        // Act
        testCommand!.HowTo = "Excecute Unit test";

        // Assert
        Assert.Equal("Excecute Unit test", testCommand.HowTo);
    }

    [Fact]
    public void CanChangePlatform()
    {
        // Arrange
    
        // Act
        testCommand!.Platform = "xUnit";
    
        // Assert
        Assert.Equal("xUnit", testCommand.Platform);
    }

    [Fact]
    public void CanChangeCommandLine()
    {
        // Arrange
    
        // Act
        testCommand!.CommandLine = "dotnet test";
    
        // Assert
        Assert.Equal("dotnet test", testCommand.CommandLine);
    }
}