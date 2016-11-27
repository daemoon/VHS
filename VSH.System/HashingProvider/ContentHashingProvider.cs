using VHS.System.FilesystemLayer;

namespace VHS.System
{
    public class ContentHashingProvider : IContentHashingProvider
    {
        private IFilesystemLayer _fsl;
        private IHashProvider _hashProvider;

        public ContentHashingProvider(IFilesystemLayer fsl, IHashProvider hashProvider)
        {
            _fsl = fsl;
            _hashProvider = hashProvider;
        }

        public string GetContentOfFileAndHashIt(string fileName)
        {
            var content = _fsl.GetByteContentsOfFile(fileName);
            return _hashProvider.HashBytes(content);
        }
    }
}