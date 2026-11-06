using DemoFunctionApp.Functions;

namespace DemoFunctionApp.Tests;

public class VulnerableFunctionsTests
{
    [Fact]
    public void HashPassword_ReturnsDeterministicValue()
    {
        Assert.Equal(
            VulnerableFunctions.HashPassword("abc"),
            VulnerableFunctions.HashPassword("abc"));
    }
}
