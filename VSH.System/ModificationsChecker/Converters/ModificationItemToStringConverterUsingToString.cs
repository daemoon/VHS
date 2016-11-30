namespace VHS.System.ModificationsChecker
{
    public class ModificationItemToStringConverterUsingToString : IModificationItemToStringConverter
    {
        public string Convert(PerFileModification item)
        {
            return item.ToString();
        }
    }
}