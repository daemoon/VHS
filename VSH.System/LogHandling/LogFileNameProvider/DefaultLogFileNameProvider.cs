namespace VHS.System.LogHandling.LogFileNameProvider
{
    public class DefaultLogFileNameProvider : ILogFileNameProvider
    {
        public string GetLogFileName()
        {
            return ".vhs";
        }
    }
}