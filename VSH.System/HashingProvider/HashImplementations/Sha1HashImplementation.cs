using System.Security.Cryptography;

namespace VHS.System
{
    public class Sha1HashImplementation : IHashImplementation
    {
        private SHA1 _sha1;
        private ByteToStringConverter _btsc;

        public Sha1HashImplementation()
        {
            _sha1 = SHA1.Create();
            _btsc = new ByteToStringConverter();
        }

        public string HashBytes(byte[] input)
        {
            var hash = _sha1.ComputeHash(input);
            return _btsc.Convert(hash);
        }
    }
}