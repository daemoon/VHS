namespace VHS.System.HashingProvider.Converters
{
    public class ByteToStringConverter 
    {
        public string Convert(byte[] input)
        {
            var result = "";
            if (input == null || input.Length == 0)
            {
                return result;
            }

            for (var index = 0; index < input.Length; index++)
            {
                var inputByte = input[index];
                result += inputByte.ToString("x2");
            }
            return result;
        }
    }
}