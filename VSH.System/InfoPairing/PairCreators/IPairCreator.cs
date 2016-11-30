namespace VHS.System
{
    public interface IPairCreator
    {
        InfoPair CreatePair(FileInfoCollector.FileInformations? newFile, FileInfoCollector.FileInformations? oldFile);
    }
}