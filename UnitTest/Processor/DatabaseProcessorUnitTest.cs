using Xunit;
using server.Processor;

namespace UnitTest.Processor
{
  public class DatabaseProcessorUnitTest
  {
    readonly DatabaseProcessor processor = new(){UnitTestMode = true};
    [Fact]
    public void TestConnectionSQLServer()
    {
      using var mysqlCon = processor.CreateConnection();
      Assert.NotNull(mysqlCon);
    }

    [Fact]
    public void TestCommandExecutionSQLServer()
    {
      using var mysqlCon = processor.CreateConnection();
      var result = processor.ExecuteSQLCommandAndRead("SELECT VERSION()");
      Assert.NotNull(result);
    }
  }
}