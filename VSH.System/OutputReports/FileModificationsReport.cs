using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using VHS.System.ModificationsChecker.Converters;
using VHS.System.ModificationsChecker.Entity;
using VHS.System.OutputReports.LineBreaker;

namespace VHS.System.OutputReports
{
    public class FileModificationsReport : OutputReport
    {
        private List<PerFileModification> items;
        private IModificationItemToStringConverter _i2s;
        private ILineBreaker _lb;


        public FileModificationsReport(List<PerFileModification> items, IModificationItemToStringConverter i2S, ILineBreaker lb)
        {
            this.items = items;
            _i2s = i2S;
            _lb = lb;
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
                var newLine = _i2s.Convert(item);
                sb.Append(_lb.AddLineBreak(newLine));
            }
            return sb.ToString();
        }
    }
}