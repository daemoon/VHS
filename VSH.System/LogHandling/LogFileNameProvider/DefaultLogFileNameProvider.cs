namespace VHS.System
{
    public class DefaultLogFileNameProvider : ILogFileNameProvider
    {
        public string GetLogFileName()
        {
            return ".vhs";
        }
    }
}