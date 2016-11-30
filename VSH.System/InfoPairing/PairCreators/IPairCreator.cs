using VHS.System.FileInfoCollectors;

namespace VHS.System.InfoPairing.PairCreators
{
    public interface IPairCreator
    {
        InfoPair CreatePair(FileInfoCollector.FileInformations? newFile, FileInfoCollector.FileInformations? oldFile);
    }
}