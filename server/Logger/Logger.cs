namespace server.Logger
{
    public class EventLogger
    {
        readonly IConfiguration Configuration = new ConfigurationBuilder()
        .AddJsonFile($"{Directory.GetCurrentDirectory()}/Logger/loggersetting.json")
        .Build();
        //Index 0 for info log, 1 for error and 2 for warning
        readonly List<string> labelList = new(){ "INFO", "ERROR", "WARN"};
        public EventLogger(){}
        public void GeneralLog(int label_idx, string logInfo){
            var logMainDirectory = Configuration["logDirectory"] ?? $"{Directory.GetCurrentDirectory()}/logs/";
            var logSubDirectory = $"{logMainDirectory}{DateTime.Now:yyyyMMdd}/";
            if(!Directory.Exists(logSubDirectory)) Directory.CreateDirectory(logSubDirectory);
            File.AppendAllText( $"{logSubDirectory}log.txt", $"{DateTime.Now.TimeOfDay}[{labelList[label_idx]}]:{logInfo}\n");
            Console.WriteLine($"Logged to {Directory.GetCurrentDirectory()}");
        }
    } 
}