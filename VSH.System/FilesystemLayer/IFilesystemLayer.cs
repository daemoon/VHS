using System.Collections.Generic;

namespace VHS.System.FilesystemLayer
{
    public interface IFilesystemLayer
    {
        List<string> GetAllFilesInDirectory(string directoryName);
        List<string> GetAllSubdirectoriesInDirectory(string directoryName);
        byte[] GetByteContentsOfFile(string fileName);
        string[] GetStringLinesOfFile(string fileName);
        void WriteFile(List<string> lines, string fileName);
    }
}