namespace VHS.System
{
    public interface IModificationClassificator
    {
        PerFileModification Classify(InfoPair pair);
    }
}