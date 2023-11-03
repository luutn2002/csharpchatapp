using server.Processor;

namespace server.Script
{
  public class UserAccountDatabaseGen{
    readonly DatabaseProcessor processor;
    public UserAccountDatabaseGen(bool DevelopmentMode = false, bool UnitTestMode = false, DatabaseProcessor? ExternalProcessor = null){
      if(ExternalProcessor == null) processor = new(DevelopmentMode: DevelopmentMode){UnitTestMode = UnitTestMode};
      else processor = ExternalProcessor;
    }
    public List<string>? CreateUserDatabase()
    {
      using var mysqlCon = processor.CreateConnection();
      processor.ExecuteSQLCommandAndRead("DROP TABLE IF EXISTS UserDatabase");
      processor.ExecuteSQLCommandAndRead(@"CREATE TABLE UserDatabase(user_id int NOT NULL AUTO_INCREMENT, 
      username varchar(255) NOT NULL, password varchar(255) NOT NULL, PRIMARY KEY (user_id), UNIQUE(user_id, username))");
      var result = processor.ExecuteSQLCommandAndRead("SHOW TABLES LIKE 'yourtable';");
      return result;
    }

    public bool AddNewUserToDatabase(string username, string password)
    {
      using var mysqlCon = processor.CreateConnection();
      var result = processor.ExecuteSQLCommandAndRead($"SELECT username FROM UserDatabase WHERE username='{username}'");
      if (result == null) processor.ExecuteSQLCommandAndRead($"INSERT INTO UserDatabase (username, password) VALUES ('{username}', '{password}')");
      else return false;
      return true;
    }
  }
}