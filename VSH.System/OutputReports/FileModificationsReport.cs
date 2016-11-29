using System.Collections.Generic;
using System.Text;

namespace VHS.System.ModificationsChecker
{
    public class FileModificationsReport : OutputReport
    {
        private List<PerFileModification> items;
        private IModificationItemToStringConverter _i2s;


        public FileModificationsReport(List<PerFileModification> items, IModificationItemToStringConverter i2S)
        {
            this.items = items;
            _i2s = i2S;
        }

        public override string Get()
        {
            if (items == null || items.Count == 0)
            {
                return "No change.";
            }
            var sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.AppendLine(_i2s.Convert(item));
            }
            return sb.ToString();
        }
    }
}