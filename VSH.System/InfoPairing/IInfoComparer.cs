namespace VHS.System
{
    public interface IInfoComparer
    {
        bool CompareInfo(FileInfoCollector.FileInformations info1, FileInfoCollector.FileInformations info2);
    }
}