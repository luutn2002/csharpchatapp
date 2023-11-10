namespace server.Processor
{
  public class UserAccountDatabaseProcessor{
    readonly DatabaseProcessor processor;
    public UserAccountDatabaseProcessor(bool DevelopmentMode = false, bool UnitTestMode = false, DatabaseProcessor? ExternalProcessor = null){
      if(ExternalProcessor == null) processor = new(DevelopmentMode: DevelopmentMode){UnitTestMode = UnitTestMode};
      else processor = ExternalProcessor;
    }

    public bool CheckIfTableExist(string TableName){
      using var mysqlCon = processor.CreateConnection();
      var reader = processor.ExecuteSQLCommandAndReturnReader($"SHOW TABLES LIKE '{TableName}'", mysqlCon);
      string result = string.Empty;

      if(reader != null)
      while (reader.Read())
      {
        result = reader.GetString(0);
      }

      if(string.IsNullOrWhiteSpace(result)) return false;
      else return true;
    }

    public void CreateUserDatabase(bool ForceOverwrite = false)
    {
      if(CheckIfTableExist("UserDatabase")){
        if(ForceOverwrite){
          using var mysqlCon = processor.CreateConnection();
          processor.ExecuteSQLCommandAndReturnReader("DROP TABLE IF EXISTS UserDatabase", mysqlCon);
          processor.ExecuteSQLCommandAndReturnReader(@"CREATE TABLE UserDatabase(user_id int NOT NULL AUTO_INCREMENT, 
          username varchar(255) NOT NULL, password varchar(255) NOT NULL, PRIMARY KEY (user_id), UNIQUE(user_id, username))", mysqlCon);
        }
      }else{
        using var mysqlCon = processor.CreateConnection();
        processor.ExecuteSQLCommandAndReturnReader(@"CREATE TABLE UserDatabase(user_id int NOT NULL AUTO_INCREMENT, 
        username varchar(255) NOT NULL, password varchar(255) NOT NULL, PRIMARY KEY (user_id), UNIQUE(user_id, username))", mysqlCon);
      }
    }

    public bool CheckIfUserExist(string username){
      using var mysqlCon = processor.CreateConnection();
      var reader = processor.ExecuteSQLCommandAndReturnReader($"SELECT username FROM UserDatabase WHERE username='{username}'", mysqlCon);
      string result = string.Empty;

      if(reader != null)
      while (reader.Read())
      {
        result = reader.GetString(0);
      }

      if(string.IsNullOrWhiteSpace(result)) return false;
      else return true;
    }

    public bool CheckIfUserValid(string username, string password){
      using var mysqlCon = processor.CreateConnection();
      var reader = processor.ExecuteSQLCommandAndReturnReader($"SELECT username, password FROM UserDatabase WHERE username='{username}'", mysqlCon);
      string name = string.Empty;
      string pass = string.Empty;

      if(reader != null)
      while (reader.Read())
      {
        name = reader.GetString(0);
        pass = reader.GetString(1);
      }

      if(string.IsNullOrWhiteSpace(name)) return false;
      else{
        if(BCrypt.Net.BCrypt.EnhancedVerify(password, pass)) return true;
        else return false;
      }
    }

    public bool AddNewUserToDatabase(string username, string password)
    {
      if(!CheckIfUserExist(username)){
        using var mysqlCon = processor.CreateConnection();
        processor.ExecuteSQLCommandAndReturnReader(@$"INSERT INTO UserDatabase (username, password) VALUES 
        ('{username}', '{BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13)}')", mysqlCon);
        return true;
      }
      else return false;
    }
  }
}