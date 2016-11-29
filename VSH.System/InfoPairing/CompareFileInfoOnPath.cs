namespace VHS.System
{
    public class CompareFileInfoOnPath : IInfoComparer
    {
        public bool CompareInfo(FileInfoCollector.FileInformations info1, FileInfoCollector.FileInformations info2)
        {
            return info1.FilePath.Equals(info2.FilePath);
        }
    }
}