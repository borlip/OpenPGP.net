using System.IO;

namespace OpenPGP.Core
{
    /// <summary>
    /// Identifies the armor on input data.
    /// </summary>
    public static class ArmorHelper
    {
        /// <summary>
        /// Determines whether the specified stream is ASCII armored.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// 	<c>true</c> if the specified stream is ASCII armored; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAsciiArmored(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var position1 = BinaryStreamSearcher.IndexOfString(stream, AsciiArmorConstants.ArmorDetectionString1);
            if (position1 >= 0)
            {
                stream.Seek(0, SeekOrigin.Begin);
                var position2 = BinaryStreamSearcher.IndexOfString(stream, AsciiArmorConstants.ArmorDetectionString2);
                return position2 > position1;
            }

            return false;
        }
    }
}
