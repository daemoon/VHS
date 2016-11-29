namespace VHS.System.ModificationsChecker
{
    public interface IModificationItemToStringConverter
    {
        string Convert(PerFileModification item);
    }
}