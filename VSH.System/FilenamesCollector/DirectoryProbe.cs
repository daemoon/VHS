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
            AddContentOfSubdirectories(ref itemsInCurrentPath);
            return itemsInCurrentPath;
        }

        private void AddFilesInCurrentPath(ref List<string> items)
        {
            var filesInCurrentDirectory = _fsl.GetAllFilesInDirectory(_basePath);
            items.AddRange(filesInCurrentDirectory);
        }

        private void AddContentOfSubdirectories(ref List<string> items)
        {
            var subdirectoriesOfCurrentDirectory = _fsl.GetAllSubdirectoriesInDirectory(_basePath);
            AddContentOfSubdirectories(ref items, subdirectoriesOfCurrentDirectory);
        }

        private void AddContentOfSubdirectories(ref List<string> items, List<string> subdirectories)
        {
            foreach (var subdirectory in subdirectories)
            {
                var subdirFiles = new DirectoryProbe(subdirectory, _fsl).Work();
                //items.AddRange(AppendSubdirectoryPathTo(subdirFiles, subdirectory));
                items.AddRange(subdirFiles);
            }
        }

        private IEnumerable<string> AppendSubdirectoryPathTo(List<string> subdirFiles, string subdirectory)
        {
            return Enumerable.Select<string, string>(subdirFiles, file => AppendSubdirPathToFile(subdirectory, file));
        }

        private static string AppendSubdirPathToFile(string subdirectory, string file)
        {
            return subdirectory + "/" + file;
        }
    }
}