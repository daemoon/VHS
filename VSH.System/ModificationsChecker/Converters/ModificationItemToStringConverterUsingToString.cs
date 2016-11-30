using VHS.System.ModificationsChecker.Entity;

namespace VHS.System.ModificationsChecker.Converters
{
    public class ModificationItemToStringConverterUsingToString : IModificationItemToStringConverter
    {
        public string Convert(PerFileModification item)
        {
            return item.ToString();
        }
    }
}