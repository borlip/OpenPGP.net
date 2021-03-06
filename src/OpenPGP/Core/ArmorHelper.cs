﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace OpenPGP.Core
{
    /// <summary>
    /// Identifies the armor on input data.
    /// </summary>
    public static class ArmorHelper
    {
        private static readonly string[] _StaticHeaderLines =
            new string[]
                {
                    AsciiArmorConstants.MessageHeaderLine,
                    AsciiArmorConstants.PrivateKeyBlockHeaderLine,
                    AsciiArmorConstants.PublicKeyBlockHeaderLine,
                    AsciiArmorConstants.SignatureHeaderLine
                };

        private static readonly Regex[] _RegexHeaderLines =
            new Regex[]
                {
                    new Regex(AsciiArmorConstants.MultipartMessageHeaderLinePattern, RegexOptions.IgnoreCase),
                    new Regex(AsciiArmorConstants.MultipartMessageHeaderLineUnspecifiedPattern, RegexOptions.IgnoreCase)
                    ,
                };

        private static readonly Regex _HeaderParserRegex = new Regex("^(?<name>.+): (?<value>.*)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Determines whether the specified stream is ASCII armored.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// 	<c>true</c> if the specified stream is ASCII armored; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAsciiArmored(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
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

        /// <summary>
        /// Determines whether the specified line is an ASCII armor header line.
        /// </summary>
        /// <param name="line">The line to evaluate.</param>
        /// <returns>
        /// 	<c>true</c> if the specified line is an ASCII armor header line; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAsciiArmorHeaderLine(string line)
        {
            if (_StaticHeaderLines.Contains(line)) return true;

            if (_RegexHeaderLines.Any(x => x.IsMatch(line))) return true;

            return false;
        }

        /// <summary>
        /// Parses a header into name/value components.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <returns><c>null</c> if the header could not be parsed; otherwise, a <see cref="Header" /> object containing the header data.</returns>
        public static Header ParseHeader(string header)
        {
            var match = _HeaderParserRegex.Match(header);
            if (match.Success)
            {
                return new Header {Name = match.Groups["name"].Value, Value = match.Groups["value"].Value};
            }
            return null;
        }
    }
}
