namespace CRUDExample.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var math = new MyMath();
        double input1 = 10;
        double input2 = 11;
        double expected = 21;

        // Fact
        double actual = math.add(input1, input2);

        // Assert
        Assert.Equal(expected, actual);
    }
}
