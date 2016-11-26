namespace VHS.System
{
    public interface IContentHashingProvider 
    {
        string GetAndHashContentOfFile(string fileName);
    }
}