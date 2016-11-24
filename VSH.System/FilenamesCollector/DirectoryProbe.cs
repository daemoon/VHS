using System.Collections.Generic;
using System.Linq;

namespace VHS.System.FilenamesCollector
{
    public class DirectoryProbe
    {
        private readonly string _basePath;
        private readonly FilesystemLayer.IFilesystemLayer _fsl;

        public DirectoryProbe(string basePath, FilesystemLayer.IFilesystemLayer fsl)
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