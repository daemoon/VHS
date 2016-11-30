using VHS.System.InfoPairing;
using VHS.System.ModificationsChecker.Entity;
using VHS.System.ModificationsChecker.Enums;

namespace VHS.System.ModificationsChecker.Classificators
{
    public class ModificationClassificator : IModificationClassificator
    {
        public PerFileModification Classify(InfoPair pair)
        {
            EFileModification modification;
            var result = new PerFileModification() {Filename = pair.FileName};
            if (pair.OldFileHash == null)
            {
                modification = EFileModification.Added;
            }
            else if (pair.NewFileHash == null)
            {
                modification = EFileModification.Removed;
            }
            else if(pair.NewFileHash.Equals(pair.OldFileHash))
            {
                modification = EFileModification.NoChange;
            }
            else
            {
                modification = EFileModification.Modified;
            }

            result.Modification = modification;
            return result;
        }
    }
}