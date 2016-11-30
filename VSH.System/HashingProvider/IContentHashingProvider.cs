namespace VHS.System.HashingProvider
{
    public interface IContentHashingProvider 
    {
        string GetContentOfFileAndHashIt(string fileName);
    }
}