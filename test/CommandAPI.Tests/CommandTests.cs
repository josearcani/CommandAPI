using CommandAPI.Models;

namespace CommandAPI.Tests;

public class CommandTests : IDisposable
{
    Command testCommand;

    public CommandTests()
    {
        testCommand = new Command
        {
            HowTo = "Do something awesome",
            Platform = "xUnit",
            CommandLine = "dotnet test"
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
        testCommand.HowTo = "Excecute Unit test";

        // Assert
        Assert.Equal("Excecute Unit test",testCommand.HowTo);
    }
}