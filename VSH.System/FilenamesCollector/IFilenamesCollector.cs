using System.Collections.Generic;

namespace VHS.System.FilenamesCollector
{
    public interface IFilenamesCollector
    {
        ICollection<string> ReturnAllFilenamesInBasePathAndSubfolders(string basePath);
    }
}