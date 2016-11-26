using System.Collections.Generic;

namespace VHS.System
{
    public interface IFilenamesCollector
    {
        ICollection<string> ReturnAllFilenamesInBasePath(string basePath);
    }
}