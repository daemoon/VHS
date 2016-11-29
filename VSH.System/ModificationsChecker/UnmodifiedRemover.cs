using System.Collections.Generic;
using System.Linq;

namespace VHS.System.ModificationsChecker
{
    public class UnmodifiedRemover
    {
        public List<PerFileModification> AlterList(List<PerFileModification> fileModificationsList)
        {
            return fileModificationsList.Where(x => x.Modification != EFileModification.NoChange).ToList();
        }
    }
}