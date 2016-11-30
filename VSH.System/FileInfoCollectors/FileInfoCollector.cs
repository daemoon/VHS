using System.Collections.Generic;
using VHS.System.FilenamesCollector;
using VHS.System.FilesystemLayer;
using VHS.System.HashingProvider;
using VHS.System.HashingProvider.HashImplementations;

namespace VHS.System.FileInfoCollectors
{
    public class FileInfoCollector
    {
        private IFilesystemLayer _fsl;
        private IFilenamesCollector _fnc;
        private IContentHashingProvider _contentHashingProvider;

        public FileInfoCollector(IFilesystemLayer fsl)
        {
            _fsl = fsl;
            _fnc = new FilenamesCollector.FilenamesCollector(_fsl);
            _contentHashingProvider = new ContentHashingProvider(_fsl, new Sha1HashImplementation());
        }

        public FileInfoCollector(IFilesystemLayer fsl, IFilenamesCollector fnc, IContentHashingProvider contentHashingProvider)
        {
            _fsl = fsl;
            _fnc = fnc;
            _contentHashingProvider = contentHashingProvider;
        }

        public List<FileInformations> CollectFileInfos(string basePath)
        {
            var fileInfos = new List<FileInformations>();
            var allFiles = _fnc.ReturnAllFilenamesInBasePathAndSubfolders(basePath);
            fileInfos.AddRange(CreateFileInfos(allFiles));
            return fileInfos; ;

        }

        private List<FileInformations> CreateFileInfos(IEnumerable<string> allFiles)
        {
            var fileInfos = new List<FileInformations>();
            foreach (var file in allFiles)
            {
                var fileContentHash = _contentHashingProvider.GetContentOfFileAndHashIt(file);
                fileInfos.Add(new FileInformations() { FilePath = file, Hash = fileContentHash});
            }
            return fileInfos;
        }

        public struct FileInformations
        {
            public string FilePath;
            public string Hash;
        }
    }
}