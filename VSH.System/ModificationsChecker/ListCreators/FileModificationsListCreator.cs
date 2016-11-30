using System.Collections.Generic;
using VHS.System.InfoPairing;
using VHS.System.ModificationsChecker.Classificators;
using VHS.System.ModificationsChecker.Entity;

namespace VHS.System.ModificationsChecker.ListCreators
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