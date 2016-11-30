using VHS.System.ModificationsChecker.Entity;

namespace VHS.System.ModificationsChecker.Converters
{
    public interface IModificationItemToStringConverter
    {
        string Convert(PerFileModification item);
    }
}