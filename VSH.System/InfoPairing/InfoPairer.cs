using System.Collections.Generic;
using System.Linq;

namespace VHS.System
{
    public class InfoPairer
    {
        private IPairCreator _ipc;
        private IInfoComparer _cmp;

        public InfoPairer(IPairCreator ipc, IInfoComparer cmp)
        {
            _ipc = ipc;
            _cmp = cmp;
        }

        public List<InfoPair> Pair(ICollection<FileInfoCollector.FileInformations> currentFileInfo, List<FileInfoCollector.FileInformations> previousLog)
        {
            var result = new List<InfoPair>();
            foreach (var fileInfo in currentFileInfo)
            {
                var isThereAMatch = previousLog.Exists(x => _cmp.CompareInfo(fileInfo, x));
                if (isThereAMatch)
                {
                    var match = previousLog.First(x => _cmp.CompareInfo(fileInfo, x));
                    result.Add(_ipc.CreatePair(fileInfo, match));
                    previousLog.Remove(match);
                }
                else
                {
                    result.Add(_ipc.CreatePair(fileInfo, null));
                }
            }
            result.AddRange(previousLog.Select(x => _ipc.CreatePair(null, x)).ToList());
            return result;
        }
    }
}