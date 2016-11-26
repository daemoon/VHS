using System.Collections.Generic;

namespace VHS.System.FilenamesCollector
{
    public class FilenamesCollector : IFilenamesCollector
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
    }
}