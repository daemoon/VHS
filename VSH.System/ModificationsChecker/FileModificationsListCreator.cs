using System.Collections.Generic;

namespace VHS.System.ModificationsChecker
{
    public class FileModificationsListCreator
    {
        private IModificationClassificator _mc;

        public FileModificationsListCreator(IModificationClassificator mc)
        {
            _mc = mc;
        }


        public List<PerFileModification> CalculateFileModifications(List<InfoPair> pairedInfos)
        {
            var result = new List<PerFileModification>();
            foreach (var pair in pairedInfos)
            {
                result.Add(_mc.Classify(pair));
            }
            return result;

        }

    }
}