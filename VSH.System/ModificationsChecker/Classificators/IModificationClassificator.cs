using VHS.System.InfoPairing;
using VHS.System.ModificationsChecker.Entity;

namespace VHS.System.ModificationsChecker.Classificators
{
    public interface IModificationClassificator
    {
        PerFileModification Classify(InfoPair pair);
    }
}