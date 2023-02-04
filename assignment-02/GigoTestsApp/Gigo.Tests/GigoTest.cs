namespace Gigo.Tests;

/*************************************
 * Bennett, Neta (netab)
 * CSHP-310
 *************************************/

[TestClass]
public class GigoTest
{
    public TestContext TestContext { get; set; }      

    [TestMethod]
    public void Scenario1()
    {
        //Arrange
        var constructorArg = new Random().Next();
        var methodArg = 0;
        var sut = new Gigo(constructorArg);
        TestContext.WriteLine($"Gigo argument {constructorArg}");

        //Act
        var result = sut.Consume(methodArg);

        //Assert
        Assert.IsTrue(result is int && (int)result == 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Scenario2()
    {
        //Arrange
        var constructorArg = new Random().Next();
        var methodArg = (double)100;
        var sut = new Gigo(constructorArg);
        TestContext.WriteLine($"Gigo argument {constructorArg}");

        //Act
        var result = sut.Consume(methodArg);

        //Assert (exception)
    }

    [DataTestMethod]
    [DataRow("a", "A")]
    [DataRow("e", "E")]
    [DataRow("i", "I")]
    [DataRow("o", "O")]
    [DataRow("u", "U")]
    [DataRow("y", null)]
    [DataRow("z", null)]
    public void Scenario3(string methodArg, string expectedResult)
    {
        //Arrange
        var constructorArg = new Random().Next();
        var sut = new Gigo(constructorArg);
        TestContext.WriteLine($"Gigo argument {constructorArg}");

        //Act
        var result = sut.Consume(methodArg);

        //Assert
        Assert.IsTrue( (result is null && expectedResult is null) ||
                        result is string && (string)result == expectedResult);
    }

    [TestMethod]
    [DynamicData(nameof(DynamicDataInput))]
    public void Scenario4(int methodArg, int expectedResult)
    {
        //Arrange
        var constructorArg = new Random().Next();
        var sut = new Gigo(constructorArg);
        TestContext.WriteLine($"Gigo argument {constructorArg}");

        //Act
        var result = sut.Consume(methodArg);

        //Assert
        Assert.IsTrue(result is int && (int)result == expectedResult);
    }

    [TestMethod]
    public void Scenario5()
    {
        //Arrange
        var constructorArg = new Random().Next();
        var methodArg = "Answer";
        var expectedResult = 42;
        var sut = new Gigo(constructorArg);
        TestContext.WriteLine($"Gigo argument {constructorArg}");

        //Act
        var result = sut.Consume(methodArg);

        //Assert
        Assert.IsTrue(result is int && (int)result == expectedResult);
    }

    [TestMethod]
    public void Scenario6()
    {
        //Arrange
        _eventFired = false;//reset at top of each run
        var constructorArg = new Random().Next();
        var methodArg = .50m;
        var sut = new Gigo(constructorArg);
        sut.DecimalDetected += ObserveDecimal;
        TestContext.WriteLine($"Gigo argument {constructorArg}");

        //Act
        var result = sut.Consume(methodArg);
        
        //Assert
        Assert.IsTrue(_eventFired);
    }

    #region Private Members
    private bool _eventFired = false;
    private void ObserveDecimal()
    {
        _eventFired = true;
    }

    private static IEnumerable<object[]> DynamicDataInput
    {
        get
        {
            return new[]
            {
                new object[] {1, 1},
                new object[] {12, 0},
                new object[] {23, 23},
                new object[] {36, 0},
                new object[] {43, 43},
                new object[] {55, 55},
                new object[] {62, 0},
                new object[] {100,0}
            };
        }
    }
    #endregion
}