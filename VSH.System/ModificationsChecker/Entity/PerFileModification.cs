using VHS.System.ModificationLabel.Factory;
using VHS.System.ModificationsChecker.Enums;

namespace VHS.System.ModificationsChecker.Entity
{
    public struct PerFileModification
    {
        public string Filename;
        public EFileModification Modification;

        public override string ToString()
        {
            return Filename + " " + new PFMModificationLabelFactory().GetLabel(Modification).Get();
        }
    }
}