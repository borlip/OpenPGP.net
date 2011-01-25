using System;
using System.Globalization;
using System.IO;

namespace OpenPGP.Core
{
    public static class BinaryStreamSearcher
    {
        private const int DefaultBufferSize = 32768;
		
        /// <summary>
        /// Returns the index of a string in a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="value">The search string.</param>
        /// <returns>The index of the string; -1 if nothing found.</returns>
		public static int IndexOfString(Stream stream, string value)
		{
			return IndexOfString(stream, value, DefaultBufferSize);
		}

        /// <summary>
        /// Returns the index of a string in a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="value">The search string.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns>The index of the string; -1 if nothing found.</returns>
        public static int IndexOfString(Stream stream, string value, int bufferSize)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (string.IsNullOrEmpty(value))
            {
                return -1;
            }

            if ((bufferSize & 1) == 1)
            {
                throw new ArgumentException("Buffer size must be even", "bufferSize");
            }
            var windowSize = bufferSize/2;
            var searchData = System.Text.Encoding.ASCII.GetBytes(value);
            if (searchData.Length > windowSize)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Length of data to find ({0}) exceeds window size of {1}", searchData.Length, windowSize));
            }
            var buffer = new byte[DefaultBufferSize];
            buffer.Initialize();

            // try to find the data in the first batch
            var bytesRead = stream.Read(buffer, 0, bufferSize);
            if (bytesRead == 0) return -1;

            var foundPosition = IndexOfBytes(buffer, searchData);
            if (foundPosition >= 0)
            {
                return foundPosition;
            }

            // read windowSize bytes of data, shifting the buffer by the much each time
            // this handles the case where the search data straddles a window boundary
            var baseIndex = 0;
            while (true)
            {
                Array.Copy(buffer, windowSize, buffer, 0, windowSize);
                baseIndex += windowSize;
                bytesRead = stream.Read(buffer, windowSize, windowSize);
                if (bytesRead == 0) return -1;
                foundPosition = IndexOfBytes(buffer, searchData);
                if (foundPosition >= 0)
                {
                    return baseIndex + foundPosition;
                }
            }
        }

        private static int IndexOfBytes(byte[] data, byte[] searchData)
        {
            for (var i = 0; i < data.Length - searchData.Length; ++i)
            {
                if (BytesExistAt(data, i, searchData))
                {
                    return i;
                }
            }
            return -1;
        }

        private static bool BytesExistAt(byte[] data, int index, byte[] searchData)
        {
            for (var i = 0; i < searchData.Length; ++i)
            {
                if (data[index + i] != searchData[i]) return false;
            }
            return true;
        }
    }
}
