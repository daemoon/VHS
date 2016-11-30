using System;

namespace VHS.System
{
    public class PairCreator : IPairCreator
    {
        private IFilenamePicker _fp;

        public PairCreator(IFilenamePicker fp)
        {
            _fp = fp;
        }

        public InfoPair CreatePair(FileInfoCollector.FileInformations? newFile, FileInfoCollector.FileInformations? oldFile)
        {
            
            return new InfoPair {FileName = _fp.GetFileName(newFile, oldFile), NewFileHash = newFile?.Hash, OldFileHash = oldFile?.Hash};
        }
    }

    public interface IFilenamePicker
    {
        string GetFileName(FileInfoCollector.FileInformations? newFile, FileInfoCollector.FileInformations? oldFile);
    }

    public class FilenamePicker : IFilenamePicker
    {
        public string GetFileName(FileInfoCollector.FileInformations? newFile, FileInfoCollector.FileInformations? oldFile)
        {
            if (newFile != null && oldFile != null)
            {
                if(newFile.Value.FilePath.Equals(oldFile.Value.FilePath))
                {
                    return newFile.Value.FilePath;
                }
                throw new FilenamePickerException();
            }
            if (newFile != null)
            {
                return newFile.Value.FilePath;
            }
            if (oldFile != null)
            {
                return oldFile.Value.FilePath;
            }
            throw new FilenamePickerException();

        }
    }

    public class FilenamePickerException : Exception
    {
        
    }
}