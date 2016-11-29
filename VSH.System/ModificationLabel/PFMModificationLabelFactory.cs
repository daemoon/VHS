using System;
using VHS.System.ModificationsChecker;

namespace VHS.System
{
    public class PFMModificationLabelFactory
    {
        public ModificationLabel GetLabel(EFileModification modification)
        {
            switch (modification)
            {
                case EFileModification.Modified:
                    return new ModifiedModificationLabel();
                case EFileModification.Removed:
                    return new RemovedModificationLabel();
                case EFileModification.Added:
                    return new AddedModificationLabel();
                case EFileModification.NoChange:
                    return new NoChangeModificationLabel();
                default:
                    throw new ArgumentOutOfRangeException(nameof(modification), modification, null);
            }

        }
    }
}