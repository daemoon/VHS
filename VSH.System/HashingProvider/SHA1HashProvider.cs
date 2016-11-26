using System.Security.Cryptography;

namespace VHS.System
{
    public class SHA1HashProvider : IHashProvider
    {
        private SHA1 _sha1;
        private ByteToStringConverter _btsc;

        public SHA1HashProvider()
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