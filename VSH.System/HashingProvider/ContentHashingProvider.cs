using VHS.System.FilesystemLayer;
using VHS.System.HashingProvider.HashImplementations;

namespace VHS.System.HashingProvider
{
    public class ContentHashingProvider : IContentHashingProvider
    {
        private IFilesystemLayer _fsl;
        private IHashImplementation _hashImplementation;

        public ContentHashingProvider(IFilesystemLayer fsl, IHashImplementation hashImplementation)
        {
            _fsl = fsl;
            _hashImplementation = hashImplementation;
        }

        public string GetContentOfFileAndHashIt(string fileName)
        {
            var content = _fsl.GetByteContentsOfFile(fileName);
            return _hashImplementation.HashBytes(content);
        }
    }
}