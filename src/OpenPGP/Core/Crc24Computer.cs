using System;

namespace OpenPGP.Core
{
    /// <summary>
    /// Computes 24-bit CRCs as specified in RFC 4880 at http://tools.ietf.org/html/rfc4880#section-6.1
    /// </summary>
    public static class Crc24Computer
    {
        private const long Initialization = 0xB704CEL;
        private const long Generator = 0x1864CFBL;

        /// <summary>
        /// Compute the CRC of a block of data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long ComputeCrc(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            if (data.Length == 0)
            {
                return 0;
            }

            var crc = Initialization;
            foreach (var t in data)
            {
                crc ^= t << 16;
                for (var j = 0; j < 8; ++j)
                {
                    crc <<= 1;
                    if ((crc & 0x1000000) != 0)
                    {
                        crc ^= Generator;
                    }
                }
            }
            return crc & 0xFFFFFFL;
        }
    }
}
