using System.Collections.Generic;

namespace VHS.System.FilenamesCollector
{
    public class FilenamesCollector
    {
        private readonly FilesystemLayer.IFilesystemLayer _filesystemLayer;

        public FilenamesCollector(FilesystemLayer.IFilesystemLayer fsl)
        {
            _filesystemLayer = fsl;
        }

        public ICollection<string> ReturnAllFilenamesInBasePath(string basePath)
        {
            var probe = new DirectoryProbe(basePath, _filesystemLayer);
            return probe.Work();
        }

        private IEnumerable<string> CollectFilesInPath(string path)
        {
            return _filesystemLayer.GetAllFilesInDirectory(path);
        }
    }
}