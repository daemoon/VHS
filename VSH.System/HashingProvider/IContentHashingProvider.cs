namespace VHS.System
{
    public interface IContentHashingProvider 
    {
        string GetContentOfFileAndHashIt(string fileName);
    }
}