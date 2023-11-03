//using System.Text;
using MySqlConnector;
using server.Logger;
using server.Processor.Base;

namespace server.Processor
{
    public class DBConnection : IDisposable
    {
        public DBConnection(){

        }
        public string? Server { get; set; }
        public string? DatabaseName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public MySqlConnection? Connection;

        public MySqlConnection GetConnection()
        {
            string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
            Connection = new MySqlConnection(connstring);
            return Connection;
        }

        public void Open()
        {
            Connection?.Open();
        }
    
        public void Dispose()
        {
            Connection?.Close();
            GC.SuppressFinalize(this);
        }        
    }
    public class DatabaseProcessor : ProcessorBase
    {
        readonly IConfiguration Configuration;
        static readonly EventLogger logger = new();
        public DatabaseProcessor(bool DevelopmentMode = false){
            Configuration = new ConfigurationBuilder()
            .AddJsonFile($"{Directory.GetCurrentDirectory()}/Processor/{(DevelopmentMode ? "dbsetting.Development" : "dbsetting")}.json")
            .Build();
        }
        public MySqlConnection? CreateConnection()
        {
            try
            {
                var dbCon = new DBConnection
                {
                    Server = Configuration["db_url"] ?? "127.0.0.1",
                    DatabaseName = Configuration["db_catalog"] ?? "db",
                    UserName = Configuration["user_id"] ?? "username",
                    Password = Configuration["password"] ?? "password"
                };
                var mysqlCon = dbCon.GetConnection();
                return mysqlCon;
            }
            catch (Exception e)
            {
                if(!UnitTestMode)logger.GeneralLog(1, e.ToString());
                else Console.WriteLine(e.ToString());
                return null;
            }
        }

        public List<string>? ExecuteSQLCommandAndRead(string query)
        {
            using MySqlConnection? mysqlCon = CreateConnection();
            if(mysqlCon != null)
            try
            {
                mysqlCon.Open();
                List<string> result = new();
                var cmd = new MySqlCommand(query, mysqlCon);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
                if(UnitTestMode)
                foreach(string line in result){
                    Console.WriteLine($"Result from Unit Testing: {line}");
                }
                return result;

            }
            catch (Exception e)
            {
                if(!UnitTestMode)logger.GeneralLog(1, e.ToString());
                else Console.WriteLine(e.ToString());
                return null;
            }
            else return null;
        }
    } 
}