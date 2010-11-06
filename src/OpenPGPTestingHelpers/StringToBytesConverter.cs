using System;

namespace OpenPGPTestingHelpers
{
    public static class StringToBytesConverter
    {
        public static byte[] ConvertToByteArray(string hex)
        {
            var result = new byte[hex.Length / 2];
            for (var i = 0; i < hex.Length; i += 2)
            {
                result[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return result;
        }
    }
}
