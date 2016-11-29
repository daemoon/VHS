using System.Collections.Generic;

namespace VHS.System
{
    public interface IFilenamesCollector
    {
        ICollection<string> ReturnAllFilenamesInBasePathAndSubfolders(string basePath);
    }
}