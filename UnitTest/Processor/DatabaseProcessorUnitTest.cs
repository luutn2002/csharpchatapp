using Xunit;
using server.Processor;

namespace UnitTest.Processor
{
  public class DatabaseProcessorUnitTest
  {
    readonly DatabaseProcessor processor = new(DevelopmentMode:true){UnitTestMode = true};
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

    [Fact]
    public void TestUserDatabaseCreate()
    {
      UserAccountDatabaseGen GenScript = new(ExternalProcessor:processor);
      var result = GenScript.CreateUserDatabase();
      Assert.NotNull(result);
    }
  }
}