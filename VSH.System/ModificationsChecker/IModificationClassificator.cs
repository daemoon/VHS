namespace VHS.System.ModificationsChecker
{
    public interface IModificationClassificator
    {
        PerFileModification Classify(InfoPair pair);
    }
}