namespace KKHHandsOnProject.Shared.Models
{
    public class LogInformation
    {
        public string Message { get; set; } = string.Empty;
        public LogData Data { get; set; }   = new LogData();
    }
}
