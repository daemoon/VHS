namespace VHS.System
{
    public class BasicLogFileNameProvider : ILogFileNameProvider
    {
        public string GetLogFileName()
        {
            return ".vhs";
        }
    }
}