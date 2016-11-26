namespace VHS.System
{
    public interface IHashProvider
    {
        string HashBytes(byte[] input);
    }
}