using System.Collections.Generic;
using System.Linq;
using VHS.System.ModificationsChecker.Entity;
using VHS.System.ModificationsChecker.Enums;

namespace VHS.System.ModificationsChecker.Removers
{
    public class UnmodifiedRemover
    {
        public List<PerFileModification> AlterList(List<PerFileModification> fileModificationsList)
        {
            return fileModificationsList.Where(x => x.Modification != EFileModification.NoChange).ToList();
        }
    }
}