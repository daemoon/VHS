using VHS.System.FileInfoCollectors;

namespace VHS.System.InfoPairing.InfoComparers
{
    public interface IInfoComparer
    {
        bool CompareInfo(FileInfoCollector.FileInformations info1, FileInfoCollector.FileInformations info2);
    }
}