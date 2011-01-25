using System;

namespace OpenPGP.Core
{
    /// <summary>
    /// Computes 24-bit CRCs as specified in RFC 4880 at http://tools.ietf.org/html/rfc4880#section-6.1
    /// </summary>
    public static class Crc24Computer
    {
        private const long InitialValue = 0xB704CEL;
        private const long Generator = 0x1864CFBL;

        /// <summary>
        /// Initializes the CRC.
        /// </summary>
        /// <returns>The initial value for the CRC.</returns>
        public static long InitializeCrc()
        {
            return InitialValue;
        }
		
        /// <summary>
        /// Compute the CRC of a block of data.
        /// </summary>
        /// <param name="data">The data to compute the CRC for.</param>
        /// <returns>The 24-bit CRC of the <paramref name="data"/>.</returns>
        public static long ComputeCrc(byte[] data)
		{
			return ComputeCrc(data, InitialValue);
		}

        /// <summary>
        /// Compute the CRC of a block of data.
        /// </summary>
        /// <param name="data">The data to compute the CRC for.</param>
        /// <param name="initialValue">The initial value of the CRC.</param>
        /// <returns>The 24-bit CRC of the <paramref name="data"/>.</returns>
        public static long ComputeCrc(byte[] data, long initialValue)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            if (data.Length == 0)
            {
                return 0;
            }

            var crc = initialValue;
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
