namespace VHS.System.HashingProvider.HashImplementations
{
    public interface IHashImplementation
    {
        string HashBytes(byte[] input);
    }
}