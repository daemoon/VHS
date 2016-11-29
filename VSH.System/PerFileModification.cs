namespace VHS.System
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