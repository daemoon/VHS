using System.Collections.Generic;
using System.Linq;
using FSL = VHS.System.FilesystemLayer;

namespace VHS.System
{
    public class FilenamesCollector
    {
        private readonly FSL.IFilesystemLayer _filesystemLayer;

        public FilenamesCollector(FSL.IFilesystemLayer fsl)
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

    public class DirectoryProbe
    {
        private readonly string _basePath;
        private readonly FSL.IFilesystemLayer _fsl;

        public DirectoryProbe(string basePath, FSL.IFilesystemLayer fsl)
        {
            _basePath = basePath;
            _fsl = fsl;
        }

        public List<string> Work()
        {
            var itemsInCurrentPath = new List<string>();
            AddFilesInCurrentPath(ref itemsInCurrentPath);
            AddSubdirectoriesInCurrentPathAndTheirContent(ref itemsInCurrentPath);
            return itemsInCurrentPath;
        }

        private void AddFilesInCurrentPath(ref List<string> items)
        {
            var filesInCurrentDirectory = _fsl.GetAllFilesInDirectory(_basePath);
            items.AddRange(filesInCurrentDirectory);
        }

        private void AddSubdirectoriesInCurrentPathAndTheirContent(ref List<string> items)
        {

            var subdirectoriesOfCurrentDirectory = _fsl.GetAllSubdirectoriesInDirectory(_basePath);
            items.AddRange(subdirectoriesOfCurrentDirectory);
            AddContentOfSubdirectories(ref items, subdirectoriesOfCurrentDirectory);
        }

        private void AddContentOfSubdirectories(ref List<string> items, List<string> subdirectories)
        {
            foreach (var subdirectory in subdirectories)
            {
                var subdirFiles = new DirectoryProbe(subdirectory, _fsl).Work();
                items.AddRange(AppendSubdirectoryPathTo(subdirFiles, subdirectory));
            }
        }

        private IEnumerable<string> AppendSubdirectoryPathTo(List<string> subdirFiles, string subdirectory)
        {
            return subdirFiles.Select(x => subdirectory + "/" + x);
        }
    }
}