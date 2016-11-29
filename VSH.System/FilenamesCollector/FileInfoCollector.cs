using System.Collections.Generic;
using VHS.System.FilesystemLayer;

namespace VHS.System
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
            _contentHashingProvider = new ContentHashingProvider(_fsl, new SHA1HashProvider());
        }

        public FileInfoCollector(IFilesystemLayer fsl, IFilenamesCollector fnc, IContentHashingProvider contentHashingProvider)
        {
            _fsl = fsl;
            _fnc = fnc;
            _contentHashingProvider = contentHashingProvider;
        }

        public ICollection<FileInformations> CollectFileInfos(string basePath)
        {
            var fileInfos = new List<FileInformations>();
            var allFiles = _fnc.ReturnAllFilenamesInBasePath(basePath);
            fileInfos.AddRange(CreateFileInfosFor(allFiles));
            return fileInfos; ;

        }

        private List<FileInformations> CreateFileInfosFor(IEnumerable<string> allFiles)
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